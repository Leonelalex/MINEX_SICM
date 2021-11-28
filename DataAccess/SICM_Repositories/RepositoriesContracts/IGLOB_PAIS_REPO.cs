using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface IGLOB_PAIS_REPO : IRepositoryBase<GLOB_PAIS>
    {
        Task<IEnumerable<GLOB_PAIS>> getPaisesConMision();
    }
}
