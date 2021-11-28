using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.RepositoryBase;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface ISICM_TIPO_CEJA_REPO : IRepositoryBase<SICM_TIPO_CEJA>
    {
        Task<bool> ExistAsync(int id);
    }
}
