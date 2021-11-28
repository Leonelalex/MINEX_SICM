using System.Collections.Generic;

namespace DataAccess.Entities.SICM_DbEntities
{
    public partial class SICM_SITUACION_ALERTA
    {


        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool isAK { get; set; }
        public bool isIC { get; set; }
    }
}
