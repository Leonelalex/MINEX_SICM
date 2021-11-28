using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccesCore
{
    public partial class SicmTipoAlerta
    {
        public SicmTipoAlerta()
        {
            SicmAlertas = new HashSet<SicmAlertas>();
        }

        public int CodigoTipoAlerta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SicmAlertas> SicmAlertas { get; set; }
    }
}
