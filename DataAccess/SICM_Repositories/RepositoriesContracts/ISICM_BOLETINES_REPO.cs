using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface ISICM_BOLETINES_REPO : IRepositoryBase<SICM_BOLETINES>
    {
        string getBoletin(int idAlerta);
    }
}
