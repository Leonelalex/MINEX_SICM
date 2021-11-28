using Core.ServiceApp.Helpers;
using System.Threading.Tasks;

namespace External.ServiceApp.Services
{
    public interface IEmailService
    {
        Task<bool> sendEmailAK(EmailModel model);
        Task<bool> sendEmailIC(EmailModel model);
        Task<bool> sendEmaiErrorlIC(EmailModel model);
        Task<bool> sendActionEmailAK(EmailModel model);
        Task<bool> sendActionEmailIC(EmailModel model);
    }
}
