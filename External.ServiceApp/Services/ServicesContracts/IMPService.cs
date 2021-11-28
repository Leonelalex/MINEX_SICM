using System.Threading.Tasks;
using static External.ServiceApp.Services.MPService;

namespace External.ServiceApp.Services.ServicesContracts
{
    public interface IMPService
    {
       Task<tokenvalid> getTokenMP();
       Task<data> getFileMP(string idFile);
    }
}
