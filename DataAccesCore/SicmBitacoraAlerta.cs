using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class SicmBitacoraAlerta
    {
        public int Codigo { get; set; }
        public int CodigoAlerta { get; set; }
        public int CodigoEstado { get; set; }
        public int CodigoAccion { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public string Adjunto { get; set; }


        public DateTime? CreadoFecha { get; set; }
        public string CreadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }
        public string EliminadoPor { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual SicmAccionesAlerta CodigoAccionNavigation { get; set; }
        public virtual SicmAlertas CodigoAlertaNavigation { get; set; }
        public virtual SicmEstadosAlerta CodigoEstadoNavigation { get; set; }
    }
}
