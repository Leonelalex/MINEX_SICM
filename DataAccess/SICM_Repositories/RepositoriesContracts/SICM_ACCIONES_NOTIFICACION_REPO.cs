using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;


namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public class SICM_ACCIONES_NOTIFICACION_REPO : RepositoryBase<SICM_ACCIONES_NOTIFICACION>, ISICM_ACCIONES_NOTIFICACION_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_ACCIONES_NOTIFICACION_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
    }
}
