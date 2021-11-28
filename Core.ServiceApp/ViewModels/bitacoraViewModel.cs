using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.ViewModels
{
    public partial class bitacoraViewModel
    {
        public int codigoAlerta { get; set; }
        public int codigoEstado { get; set; }
        public int codigoAccion { get; set; }
        public DateTime fecha { get; set; }
        public string observaciones { get; set; }
        public string adjunto { get; set; }
        public int? codigoAccionNotificacion { get; set; }
        public string usuario { get; set; }
        public string destinatarios { get; set; }
        public string pais { get; set; }
    }
}
