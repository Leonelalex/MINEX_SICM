using DataAccess.Entities.SICM_DbEntities.Sistema;
using System.Threading.Tasks;

namespace Core.ServiceApp.Utils.UtilContracts
{
    public interface ILogUtils
    {
        Task<bool> SaveLogError(SICM_SYSLOG model);
    }
}
