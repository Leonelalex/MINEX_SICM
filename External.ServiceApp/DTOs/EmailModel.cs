using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.Helpers
{
    public class EmailModel
    {
        public int codAlerta { get; set; }
        public string numeroAlerta { get; set; }
        public string listCorreos { get; set; }
        public string listDocs { get; set; }
        public string oficio { get; set; }
        public string boletinIC { get;set; }
        public string subjet { get; set; }
        public string body { get; set; }
        public string titulo { get; set; }
        public string saludo { get; set; }
        public string datosAlerta { get; set; }

    }
}
