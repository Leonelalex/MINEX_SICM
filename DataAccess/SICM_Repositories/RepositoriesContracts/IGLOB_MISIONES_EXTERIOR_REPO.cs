using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.Helpers.ViewModels;
using DataAccess.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface IGLOB_MISIONES_EXTERIOR_REPO : IRepositoryBase<GLOB_MISIONES_EXTERIOR>
    {
        Task<List<misionesViewModel>> getMisiones();

        Task<List<emailsMisiones>> GetEmails(List<int> IdMisiones);
    }
}
