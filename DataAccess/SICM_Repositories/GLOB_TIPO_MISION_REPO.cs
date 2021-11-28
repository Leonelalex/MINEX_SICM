using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;

namespace DataAccess.SICM_Repositories
{
    public class GLOB_TIPO_MISION_REPO : RepositoryBase<GLOB_TIPO_MISION>, IGLOB_TIPO_MISION_REPO
    {
        public GLOB_TIPO_MISION_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
        }
    }
}
