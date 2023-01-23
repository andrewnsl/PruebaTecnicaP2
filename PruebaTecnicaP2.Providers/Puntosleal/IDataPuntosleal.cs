using PruebaTecnicaP2.Models.Dtos;
using PruebaTecnicaP2.Models.Entities;
using PruebaTecnicaP2.Models.PayLoads;
using PruebaTecnicaP2.Models.Providers.Puntosleal;

namespace PruebaTecnicaP2.Providers.Puntosleal
{
    public interface IDataPuntosleal
    {
        Task<ResponseServiceDto<CargarPuntosleal>> CargarPuntos(PuntosLoad puntos);
    }
}