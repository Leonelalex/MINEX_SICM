using Core.ServiceApp.Helpers;
using Core.ServiceApp.ViewModels;
using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services.ServiceContracts
{
    public interface ISearchModuleService
    {
        Task<Response> buscarPersonaLocal(persona_ViewModel persona, PaginationFilter pagefilter);
        Task<Response> buscarAlertaporCodigo(string codigoAlerta);
    }
}
