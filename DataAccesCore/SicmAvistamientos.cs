using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class SicmAvistamientos
    {
        public int CodigoAvistamiento { get; set; }
        public string Motivo { get; set; }
        public string CodigoUsuario { get; set; }
        public int CodigoAlerta { get; set; }
        public bool? Anonimo { get; set; }
        public string Correo { get; set; }
        public string Observaciones { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaHora { get; set; }
        public int? CodigoMunicipio { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public string CreadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime? EliminadoFecha { get; set; }
        public string EliminadoPor { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual SicmAlertas CodigoAlertaNavigation { get; set; }
        public virtual GlobCiudad CodigoMunicipioNavigation { get; set; }
    }
}
