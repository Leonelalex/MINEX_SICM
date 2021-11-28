using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.RepositoryBase;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface ISICM_COLOR_CABELLO_REPO : IRepositoryBase<SICM_COLOR_CABELLO>
    {
        Task<bool> ExistAsync(int id);
    }
}
