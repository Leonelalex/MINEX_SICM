using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class SICM_ESTADOS_ALERTA_REPO : RepositoryBase<SICM_ESTADOS_ALERTA>, ISICM_ESTADOS_ALERTAS_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_ESTADOS_ALERTA_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
    }
}
