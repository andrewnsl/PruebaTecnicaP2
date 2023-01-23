using PruebaTecnicaP2.Models.Dtos;
using PruebaTecnicaP2.Models.PayLoads;

namespace PruebaTecnicaP2.Services
{
    public interface IPuntosService
    {
        Task<ResponseServiceDto<bool>> CargarPuntos(List<PuntosLoad> puntosLoads);
        Task<ResponseServiceDto<List<PuntosDto>>> ObtenerPuntos();
    }
}