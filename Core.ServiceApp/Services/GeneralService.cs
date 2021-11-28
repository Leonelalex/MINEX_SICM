using DataAccess.DBContexts;
using System.Collections.Generic;

namespace Core.ServiceApp.Services
{
    public class GeneralService
    {
        private readonly SICM_DBContext DbContext;
        public object getCatalogs()
        {
            var list = new List<dynamic>();
            return list;
        }
    }
}
