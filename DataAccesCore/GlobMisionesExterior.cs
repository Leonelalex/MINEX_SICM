using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class GlobMisionesExterior
    {
        public int IdMisionExterior { get; set; }
        public string NombreMision { get; set; }
        public string NombreMisionTransferencia { get; set; }
        public int CodigoTipoMision { get; set; }
        public int? CodigoPais { get; set; }
        public int? CodigoCiudad { get; set; }
        public int? CodigoDepartamento { get; set; }
        public string Direccion { get; set; }
        public string Zip { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string CorreoElectronico { get; set; }
        public string SitioWeb { get; set; }
        public string CircunscripcionSeccionConsular { get; set; }
        public DateTime? FechaAcreditacion { get; set; }
        public string Horario { get; set; }
        public string FiestaNacional { get; set; }
        public int? Moneda { get; set; }
        public string Notas { get; set; }

        public virtual GlobTipoMision CodigoTipoMisionNavigation { get; set; }
        public virtual SicmAlertaMisiones IdMisionExteriorNavigation { get; set; }
    }
}
