using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace nuevodatacces
{
    public partial class SicmBoletines
    {
        public int CodigoBoletin { get; set; }
        public int CodigoAlerta { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaHoraDesaparicion { get; set; }
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
        public string Vestimenta { get; set; }
        public int? Estatura { get; set; }
        public int? Genero { get; set; }
        public string SeniasParticulares { get; set; }
        public int? Nacionalidad { get; set; }

        public virtual SicmAlertas CodigoAlertaNavigation { get; set; }
    }
}
