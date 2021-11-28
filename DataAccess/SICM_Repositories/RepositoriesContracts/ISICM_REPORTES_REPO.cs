using DataAccess.Helpers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface ISICM_REPORTES_REPO
    {
        Task<List<EstatusPorAño_VM>> EstatusPorAño(int año);
        Task<List<BoletinesMes_VM>> BoletinesPorMes(int año, int genero, int estado);


    }
}
