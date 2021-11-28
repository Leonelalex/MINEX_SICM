using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Helpers.ViewModels
{
    public class UltimasAcciones
    {
        public int codigoAlerta { get; set; }
        public string numeroAlerta { get; set; }
        public int codigoAccion { get; set; }
        public int codigoEstado { get; set; }
        public DateTime? fechaAccion { get; set; }
        public string observaciones { get; set; }
        public string adjunto { get; set; }
        public string usuario { get; set; }
        public int? codigoNotificacion { get; set; }
        public string destinatarios { get; set; }
        public int estadoAlerta { get; set; }
        public string numeroOficio { get; set; }
        public string numeroCaso { get; set; }
        public int tipoAlerta { get; set; }
    }
}
