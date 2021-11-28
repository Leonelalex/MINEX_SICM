using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.Helpers;
using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services.ServiceContracts
{
    public interface IActionService
    {
        Task<Response> notificarAutoridades(NotificarAutoridades_DTO model, int codAlerta);
        Task<Response> avistamiento(Avistamiento_DTO model, int codAlerta);
        Task<Response> cambioSituacion(CambioSituacion_DTO model, int codAlerta);
        Task<Response> GetUltimasAcciones(UltimasAcciones_DTO model, PaginationFilter filter);
    }
}
