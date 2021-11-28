using Core.ServiceApp.DTO.Core_DTOs;
using Core.ServiceApp.DTO.Core_DTOs.Catalogos_DTOS;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.ViewModels;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services.ServiceContracts
{
    public interface ICatalogosService
    {

        Task<Response> GetAllCatalogos();
        Task<Response> GetAllSituacion();
        Task<Response> PostSituacion(CatalogoGenerico_DTO model);
        Task<Response> PutSituacion(CatalogoGenerico_DTO model, int codSituacion);
        Task<Response> DeleteSituacion(int codSituacion);

        Task<Response> GetAllEstatus();
        Task<Response> PostEstatus(CatalogoGenerico_DTO model);
        Task<Response> PutEstatus(CatalogoGenerico_DTO model, int codEstatus);
        Task<Response> DeleteEstatus(int codEstatus);

        Task<Response> GetAllAccionesNotificacion();
        Task<Response> PostAccionNotificacion(CatalogoGenerico_DTO model);
        Task<Response> PutAccionNotificacion(CatalogoGenerico_DTO model, int codEstatus);
        Task<Response> DeleteAccionNotificacion(int codAccion);

        Task<Response> GetAllParametros();
        Task<Response> PutParametrosGenerales(ParametrosGenerales_DTO model, int codParam);

    }
}
