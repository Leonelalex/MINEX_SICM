using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.RepositoryBase;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface ISICM_TEZ_REPO : IRepositoryBase<SICM_TEZ>
    {
        // Implementa los métodos base del repositorio INorthwindDBRepositoryBase : Get,Add,Edit,Remove,GetAll,Find,FindSingleOrDefault,FindFirstOrDefault,Exist
        // Agregar los metodos personalizados
        Task<bool> ExistAsync(int id);
    }
}
