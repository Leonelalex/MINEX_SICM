using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface ISICM_ALERTA_MISIONES_REPO : IRepositoryBase<SICM_ALERTA_MISIONES>
    {
        Task DeleteByCodigoAlerta(int codAlerta);


    }
}
