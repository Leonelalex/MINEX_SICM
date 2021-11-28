using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;

namespace DataAccess.SICM_Repositories
{
    public class SICM_SYSLOG_REPO : RepositoryBase<SICM_SYSLOG>, ISICM_SYSLOG_REPO
    {
        private readonly ISICM_DBContext _dbContext;
        public SICM_SYSLOG_REPO(ISICM_DBContext dBContext) : base(dBContext) {
            _dbContext = dBContext;
        }
    }
}
