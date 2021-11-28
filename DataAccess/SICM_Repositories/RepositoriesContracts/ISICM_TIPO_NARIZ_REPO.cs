using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.RepositoryBase;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface ISICM_TIPO_NARIZ_REPO : IRepositoryBase<SICM_TIPO_NARIZ>
    {
        Task<bool> ExistAsync(int id);
    }
}
