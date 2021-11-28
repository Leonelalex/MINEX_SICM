using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.SICM_DbEntities.Sistema
{
    public class SICM_SYSLOG
    {
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Url { get; set; }
        public string Exception { get; set; }
        public string ExceptionType { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public string IpAdress { get; set; }

    }
}
