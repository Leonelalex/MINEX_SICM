using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System.Linq;

namespace DataAccess.SICM_Repositories
{
    public class SICM_BOLETINES_REPO : RepositoryBase<SICM_BOLETINES>, ISICM_BOLETINES_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_BOLETINES_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public string getBoletin(int idAlerta)
        {
            try
            {
                return _dbContext.SICM_BOLETINES.Where(x => x.codigoAlerta == idAlerta).Select(x => x.boletin).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
