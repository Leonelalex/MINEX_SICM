using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class SicmAlertas
    {
        public SicmAlertas()
        {
            SicmAlertaMisiones = new HashSet<SicmAlertaMisiones>();
            SicmAvistamientos = new HashSet<SicmAvistamientos>();
            SicmBitacoraAlerta = new HashSet<SicmBitacoraAlerta>();
        }

        public int Codigo { get; set; }
        public string CodigoAlerta { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime? FechaHora { get; set; }
        public string Direccion { get; set; }
        public int? CodigoMunicipio { get; set; }
        public int? CodigoPais { get; set; }
        public string Foto { get; set; }
        public int? CodigoColorCabello { get; set; }
        public int? CodigoTipoCabello { get; set; }
        public int? CodigoTamanioCabello { get; set; }
        public int? CodigoTipoCeja { get; set; }
        public int? CodigoColorOjos { get; set; }
        public int? CodigoTipoNariz { get; set; }
        public int? CodigoComplexion { get; set; }
        public int? CodigoTez { get; set; }
        public string Observaciones { get; set; }
        public int? EstadoAlerta { get; set; }
        public DateTime? FechaActivacion { get; set; }
        public string Vestimenta { get; set; }
        public int? Estatura { get; set; }
        public string CodigoUsuario { get; set; }
        public int Genero { get; set; }
        public string SeniasParticulares { get; set; }
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

        public virtual SicmColorCabello CodigoColorCabelloNavigation { get; set; }
        public virtual SicmColorOjos CodigoColorOjosNavigation { get; set; }
        public virtual SicmComplexion CodigoComplexionNavigation { get; set; }
        public virtual GlobCiudad CodigoMunicipioNavigation { get; set; }
        public virtual SicmSituacionAlerta CodigoSituacionNavigation { get; set; }
        public virtual SicmTamanioCabello CodigoTamanioCabelloNavigation { get; set; }
        public virtual SicmTez CodigoTezNavigation { get; set; }
        public virtual SicmTipoAlerta CodigoTipoAlertaNavigation { get; set; }
        public virtual SicmTipoCabello CodigoTipoCabelloNavigation { get; set; }
        public virtual SicmTipoCeja CodigoTipoCejaNavigation { get; set; }
        public virtual SicmTipoNariz CodigoTipoNarizNavigation { get; set; }
        public virtual SicmEstadosAlerta EstadoAlertaNavigation { get; set; }
        public virtual GlobGenero GeneroNavigation { get; set; }
        public virtual ICollection<SicmAlertaMisiones> SicmAlertaMisiones { get; set; }
        public virtual ICollection<SicmAvistamientos> SicmAvistamientos { get; set; }
        public virtual ICollection<SicmBitacoraAlerta> SicmBitacoraAlerta { get; set; }
    }
}
