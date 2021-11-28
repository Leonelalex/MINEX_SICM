using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.RepositoryBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface ISICM_ALERTAS_REPO : IRepositoryBase<SICM_ALERTAS>
    {
        Task<SICM_ALERTAS> getOjAllByCodeAlert(string CodeAlert);
        Task<IEnumerable<SICM_ALERTAS>> getAlertasAlbaKeneth(PaginationFilter filter);
        Task<IEnumerable<SICM_ALERTAS>> getAllAlbaKeneth();
        Task<IEnumerable<SICM_ALERTAS>> getAlertasIsabelClaudina(PaginationFilter filter);
        Task<IEnumerable<SICM_ALERTAS>> getAllIsabelClaudina();
        int countAlertasAlbaKeneth();
        int countAlertasIsabelClaudina();
        Task<SICM_ALERTAS> getAlertaByCodigo(string codigoAlerta);
        Task<IEnumerable<SICM_ALERTAS>> getAlertaICSinActivar();
    }
}
