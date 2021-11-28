using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface IDEPARTAMENTOS_REPO : IRepositoryBase<GLOB_DIVISION>
    {
        // Implementa los métodos base del repositorio INorthwindDBRepositoryBase : Get,Add,Edit,Remove,GetAll,Find,FindSingleOrDefault,FindFirstOrDefault,Exist
        Task<IEnumerable<GLOB_DIVISION>> getByPais(int codigoPais);
    }
}
