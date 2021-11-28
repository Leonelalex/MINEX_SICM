using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class GlobCiudad
    {
        public GlobCiudad()
        {
            SicmAlertas = new HashSet<SicmAlertas>();
            SicmAvistamientos = new HashSet<SicmAvistamientos>();
        }

        public int CodigoCiudad { get; set; }
        public string Descripcion { get; set; }
        public int HusoHorario { get; set; }
        public int CodigoDivision { get; set; }

        public virtual GlobDivision CodigoDivisionNavigation { get; set; }
        public virtual ICollection<SicmAlertas> SicmAlertas { get; set; }
        public virtual ICollection<SicmAvistamientos> SicmAvistamientos { get; set; }
    }
}
