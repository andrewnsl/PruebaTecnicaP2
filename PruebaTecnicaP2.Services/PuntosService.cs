using AutoMapper;
using PruebaTecnicaP2.DataAccess.Repository;
using PruebaTecnicaP2.Helpers.Extensions;
using PruebaTecnicaP2.Helpers.LoggerManager;
using PruebaTecnicaP2.Models.Dtos;
using PruebaTecnicaP2.Models.Entities;
using PruebaTecnicaP2.Models.Enums;
using PruebaTecnicaP2.Models.PayLoads;
using PruebaTecnicaP2.Models.Providers.Puntosleal;
using PruebaTecnicaP2.Providers.Puntosleal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Services
{
    public class PuntosService : IPuntosService
    {
        private readonly IDataRepository<Puntos> _puntosRepo;
        private readonly IDataPuntosleal _dataPuntosleal;
        private readonly IMapper _mapper;
        private readonly ILog _log;

        public PuntosService(ILog log, IDataRepository<Puntos> puntosRepo, IMapper mapper, IDataPuntosleal dataPuntosleal)
        {
            _log = log;
            _puntosRepo = puntosRepo;
            _mapper = mapper;
            _dataPuntosleal = dataPuntosleal;
        }

        public async Task<ResponseServiceDto<bool>> CargarPuntos(List<PuntosLoad> puntosLoads)
        {
            ResponseServiceDto<bool> responseGenericDto = new ResponseServiceDto<bool>();
            List<Puntos> puntosInsert = new List<Puntos>();
            try
            {
                if (puntosLoads.Any())
                {

                    foreach (PuntosLoad item in puntosLoads)
                    {
                        ResponseServiceDto<CargarPuntosleal> response = await _dataPuntosleal.CargarPuntos(item);
                        if (response.Code == TypeMessage.Succes)
                        {
                            Puntos punto = _mapper.Map<Puntos>(item);
                            punto.id_transaccion = response.Result!.id_transaccion;
                            puntosInsert.Add(punto);
                        }
                    }

                    await _puntosRepo.AddRange(puntosInsert);

                    return await responseGenericDto.GetResultSucces();
                }
                else
                {
                    return await responseGenericDto.GetResultError();
                }

            }
            catch (Exception e)
            {
                _log.LogError(e);
                return await responseGenericDto.GetResultError();
            }
        }

        public async Task<ResponseServiceDto<List<PuntosDto>>> ObtenerPuntos()
        {
            ResponseServiceDto<List<PuntosDto>> responseGenericDto = new ResponseServiceDto<List<PuntosDto>>();
            try
            {
                List<Puntos> puntos = await _puntosRepo.List();

                return await responseGenericDto.GetResultSucces(_mapper.Map<List<PuntosDto>>(puntos));
            }
            catch (Exception e)
            {
                _log.LogError(e);
                return await responseGenericDto.GetResultError();
            }
        }
    }
}
