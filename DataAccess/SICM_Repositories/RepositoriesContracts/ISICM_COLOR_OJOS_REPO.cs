using DataAccess.RepositoryBase;
using DataAccess.Entities.SICM_DbEntities.Catalogs;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface ISICM_COLOR_OJOS_REPO : IRepositoryBase<SICM_COLOR_OJOS>
    {
        Task<bool> ExistAsync(int id);
    }
}
