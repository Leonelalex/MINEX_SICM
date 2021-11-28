using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.Helpers;
using DataAccess.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services.ServiceContracts
{
    public interface IAlbaKenethService 
    {
        Task<Response> CrearAlerta(AlertaAlbaKeneth_DTO model);
        Task<Response> GetAllAlerts(PaginationFilter filter);
        Task<Response> GetBitacora(int codAlerta);
        Task<Response> UpdateAlerta(AlertaAlbaKeneth_DTO model, int codAlerta);
        Task<Response> DeleteBoletin(int codBoletin);
        Task<Response> AddBoletin(BoletinAK_DTO boletin, int codAlerta);
        Task<Response> EditBoletin(BoletinAK_DTO model, int codBoletin);
        Task<Response> DesactivarAlerta(DesactivarAlerta_AK_DTO model, int codAlerta);
        Task<Response> GetAlertsByFilters(AlertSearchFilters filters, PaginationFilter pagefilter);
    }
}
