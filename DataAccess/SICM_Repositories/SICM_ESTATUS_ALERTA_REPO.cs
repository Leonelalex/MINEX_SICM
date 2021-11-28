using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class SICM_ESTATUS_ALERTA_REPO : RepositoryBase<SICM_ESTATUS_ALERTA>, ISICM_ESTATUS_ALERTA_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_ESTATUS_ALERTA_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public string GetNameByIdAsync(int id)
        {
            return _dbContext.SICM_ESTATUS_ALERTA.Where(x => x.codigo == id).First<SICM_ESTATUS_ALERTA>().nombre;
        }
    }
}
