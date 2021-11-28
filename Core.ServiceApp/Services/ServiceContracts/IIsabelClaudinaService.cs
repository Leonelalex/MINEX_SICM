using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.DTO.Core_DTOs.Responses;
using Core.ServiceApp.Helpers;
using DataAccess.Helpers;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services.ServiceContracts
{
    public interface IIsabelClaudinaService 
    {
        Task<Response> GetBitacora(int codAlerta);
        Task<AlertaIsabelClaudina_ViewModel> CreateAlert(AlertaIsabelClaudina_DTO model);
        Task<AlertaIsabelClaudina_ViewModel> DesactiveAlert(AlertaIsaChangeStatusDTO model);
        Task<Response> GetAll(PaginationFilter filter);
        Task<Response> ActivateAlert(Activar_IC_DTO misiones, int codAlerta);
        Task<Response> GetAlertasSinActivar(PaginationFilter filter);
        Task<Response> diffusionIC(DiffusionIC_DTO model, int codAlerta);
        Task<Response> GetAlertsByFilters(AlertSearchFilters filters, PaginationFilter pagefilter);
    }
}
