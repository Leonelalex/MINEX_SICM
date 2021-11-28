using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;

namespace DataAccess.SICM_Repositories
{
    public class GLOB_GENERO_REPO : RepositoryBase<GLOB_GENERO>, IGLOB_GENERO_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public GLOB_GENERO_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
    }
}
