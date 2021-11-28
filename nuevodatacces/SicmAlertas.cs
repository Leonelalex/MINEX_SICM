using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace nuevodatacces
{
    public partial class SicmAlertas
    {
        public SicmAlertas()
        {
            SicmAlertaMisiones = new HashSet<SicmAlertaMisiones>();
            SicmAvistamientos = new HashSet<SicmAvistamientos>();
            SicmBoletines = new HashSet<SicmBoletines>();
        }

        public int Codigo { get; set; }
        public string CodigoAlerta { get; set; }
        public int? CodigoMunicipio { get; set; }
        public string Direccion { get; set; }
        public string Observaciones { get; set; }
        public int? EstadoAlerta { get; set; }
        public DateTime? FechaActivacion { get; set; }
        public DateTime? FechaDesactivacion { get; set; }
        public string CodigoUsuario { get; set; }
        public string CodigoOficio { get; set; }
        public string Oficio { get; set; }
        public int CodigoTipoAlerta { get; set; }
        public int CodigoSituacion { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public string CreadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }
        public string EliminadoPor { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<SicmAlertaMisiones> SicmAlertaMisiones { get; set; }
        public virtual ICollection<SicmAvistamientos> SicmAvistamientos { get; set; }
        public virtual ICollection<SicmBoletines> SicmBoletines { get; set; }
    }
}
