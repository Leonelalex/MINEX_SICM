using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;

namespace DataAccess.SICM_Repositories
{
    public class SICM_ACCIONES_ALERTA_REPO : RepositoryBase<SICM_ACCIONES_ALERTA>, ISICM_ACCIONES_ALERTA_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_ACCIONES_ALERTA_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
    }
}
