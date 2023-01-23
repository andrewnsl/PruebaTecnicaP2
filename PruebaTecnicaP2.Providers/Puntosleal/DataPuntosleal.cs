using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PruebaTecnicaP2.Models.Dtos;
using PruebaTecnicaP2.Models.Enums;
using PruebaTecnicaP2.Models.Helpers;
using PruebaTecnicaP2.Helpers.Extensions;
using PruebaTecnicaP2.Models.Providers.Puntosleal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnicaP2.Models.Entities;
using PruebaTecnicaP2.Models.PayLoads;

namespace PruebaTecnicaP2.Providers.Puntosleal
{
    public class DataPuntosleal : IDataPuntosleal
    {
        private readonly PuntoslealSettings _puntoslealSettings;
        #region Builder
        public DataPuntosleal(IOptions<PuntoslealSettings> puntoslealSettings)
        {
            _puntoslealSettings = puntoslealSettings.Value;
        }
        #endregion

        public async Task<ResponseServiceDto<CargarPuntosleal>> CargarPuntos(PuntosLoad puntos)
        {
            ResponseServiceDto<CargarPuntosleal> result = new ResponseServiceDto<CargarPuntosleal>();


            RestClient? _RestClient = await ConexionWebAPis();
            if (_RestClient != null)
            {
                RestRequest request = new RestRequest($"/usu_historial_puntos/cargar_puntos", Method.Post);
                request.Timeout = 10000000;
                request.AddBody(puntos);


                RestResponse response = await _RestClient.ExecuteAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    CargarPuntosleal obj = JsonConvert.DeserializeObject<CargarPuntosleal>(response.Content!)!;
                    result = await result.GetResultSucces(obj);
                }
                else
                {
                    result = await result.GetResultError();
                    result.Message = response.StatusCode.ToString();
                }

                return result;
            }
            else
            {
                return await result.GetResultError();
            }

        }

        private async Task<ResponseServiceDto<LoginPuntosleal>> Login()
        {
            ResponseServiceDto<LoginPuntosleal> result = new ResponseServiceDto<LoginPuntosleal>();

            RestClient _RestClient = new(_puntoslealSettings.Url);
            RestRequest request = new RestRequest($"/com_usuarios/login", Method.Post);
            request.Timeout = 10000000;
            request.AddBody(new { usuario = _puntoslealSettings.Usuario, contrasena = _puntoslealSettings.Contrasena });


            RestResponse response = await _RestClient.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                LoginPuntosleal obj = JsonConvert.DeserializeObject<LoginPuntosleal>(response.Content!)!;
                result = await result.GetResultSucces(obj);
            }
            else
            {
                result = await result.GetResultError();
                result.Message = response.StatusCode.ToString();
            }

            return result;
        }

        private async Task<RestClient?> ConexionWebAPis()
        {
            ResponseServiceDto<LoginPuntosleal> login = await Login();
            if (login.Code == TypeMessage.Succes)
            {
                RestClient _RestClient = new(_puntoslealSettings.Url);
                _RestClient.AddDefaultHeader("Authorization", $"Bearer {login.Result!.token}");
                return _RestClient;
            }
            else
            {
                return null;
            }

        }
    }
}
