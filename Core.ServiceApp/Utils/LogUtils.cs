using Core.ServiceApp.Utils.UtilContracts;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Core.ServiceApp.Utils
{
    public class LogUtils : ILogUtils
    {
        private readonly ISICM_SYSLOG_REPO _ISICM_SYSLOG_REPO;

        public LogUtils(ISICM_SYSLOG_REPO SICM_SYSLOG_REPO)
        {
            _ISICM_SYSLOG_REPO = SICM_SYSLOG_REPO;
        }
        public async Task<bool> SaveLogError(SICM_SYSLOG model)
        {
            try
            {
                model.Logged = DateTime.Now;
                await _ISICM_SYSLOG_REPO.Add(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetLocalIpAdress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return null;
        }


    }

}