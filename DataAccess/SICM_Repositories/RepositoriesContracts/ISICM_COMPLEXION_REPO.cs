using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.RepositoryBase;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface ISICM_COMPLEXION_REPO : IRepositoryBase<SICM_COMPLEXION>
    {
        Task<bool> ExistAsync(int id);
    }
}
