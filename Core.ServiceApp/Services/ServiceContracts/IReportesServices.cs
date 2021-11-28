using Core.ServiceApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services.ServiceContracts
{
    public interface IResportesService
    {
        Task<Response> getReporteEstatuspoAño();
        Task<Response> getReporteBoletinesPorMes(int año, int estatus);
    }
}
