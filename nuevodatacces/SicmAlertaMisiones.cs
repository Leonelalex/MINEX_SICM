using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace nuevodatacces
{
    public partial class SicmAlertaMisiones
    {
        public int CodigoAlertaMision { get; set; }
        public int CodigoAlerta { get; set; }
        public int CodigoMision { get; set; }

        public virtual SicmAlertas CodigoAlertaNavigation { get; set; }
    }
}
