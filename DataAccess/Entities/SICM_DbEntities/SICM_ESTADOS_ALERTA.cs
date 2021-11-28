using System.Collections.Generic;

namespace DataAccess.Entities.SICM_DbEntities
{
    public partial class SICM_ESTADOS_ALERTA
    {
        public SICM_ESTADOS_ALERTA()
        {
            SicmAlertas = new HashSet<SICM_ALERTAS>();
            SicmBitacoraAlerta = new HashSet<SICM_BITACORA_ALERTA>();
        }

        public int CodigoEstado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SICM_ALERTAS> SicmAlertas { get; set; }
        public virtual ICollection<SICM_BITACORA_ALERTA> SicmBitacoraAlerta { get; set; }
    }
}
