using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class GlobPais
    {
        public GlobPais()
        {
            GlobDivision = new HashSet<GlobDivision>();
        }

        public int CodigoPais { get; set; }
        public string Descripcion { get; set; }
        public int HusoHorario { get; set; }
        public string CodigoIsoAlpha3 { get; set; }
        public int CodigoRegion { get; set; }
        public int CodigoMoneda { get; set; }
        public DateTime? FechaInicioRelaciones { get; set; }
        public string Nacionalidad { get; set; }
        public byte[] ImgBandera { get; set; }
        public string CategoriaOrdinaria { get; set; }
        public string CategoriaOficial { get; set; }
        public byte[] ImgMapa { get; set; }
        public string HtmlReloj { get; set; }

        public virtual ICollection<GlobDivision> GlobDivision { get; set; }
    }
}
