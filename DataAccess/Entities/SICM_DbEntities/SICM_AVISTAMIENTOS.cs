using DataAccess.Entities.SICM_DbEntities.Generales;
using System;

namespace DataAccess.Entities.SICM_DbEntities
{
    public partial class SICM_AVISTAMIENTOS
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

        public virtual SICM_ALERTAS CodigoAlertaNavigation { get; set; }
        public virtual GLOB_CIUDAD CodigoMunicipioNavigation { get; set; }
    }
}
