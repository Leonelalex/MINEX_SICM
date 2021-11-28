using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities.SICM_DbEntities
{
    public class SICM_ESTATUS_ALERTA 
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
        public bool isAK { get; set; }
        public bool isIC { get; set; }
    }
}
