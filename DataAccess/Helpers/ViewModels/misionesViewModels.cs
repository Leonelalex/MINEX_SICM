using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Helpers.ViewModels
{
    public class misionesViewModel
    {
        public int ID_MISION_EXTERIOR { get; set; }
        public string NOMBRE_MISION { get; set; }
        public string NOMBRE_TIPO_MISION { get; set; }
        public int CODIGO_PAIS { get; set; }
        public int PRIORIDAD { get; set; }
    }
}
