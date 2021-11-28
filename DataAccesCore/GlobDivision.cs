using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class GlobDivision
    {
        public GlobDivision()
        {
            GlobCiudad = new HashSet<GlobCiudad>();
        }

        public int CodigoDivision { get; set; }
        public string Descripcion { get; set; }
        public int CodigoPais { get; set; }

        public virtual GlobPais CodigoPaisNavigation { get; set; }
        public virtual ICollection<GlobCiudad> GlobCiudad { get; set; }
    }
}
