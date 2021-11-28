using System.Collections.Generic;

namespace DataAccess.Entities.SICM_DbEntities
{
    public partial class SICM_ACCIONES_ALERTA
    {
        public SICM_ACCIONES_ALERTA()
        {
            SicmBitacoraAlerta = new HashSet<SICM_BITACORA_ALERTA>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ContenidoEmail { get; set; }

        public virtual ICollection<SICM_BITACORA_ALERTA> SicmBitacoraAlerta { get; set; }
    }
}
