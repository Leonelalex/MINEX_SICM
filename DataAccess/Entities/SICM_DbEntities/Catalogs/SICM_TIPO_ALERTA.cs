
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DataAccess.Entities.SICM_DbEntities.Catalogs
{
    public class SICM_TIPO_ALERTA
    {
        public SICM_TIPO_ALERTA()
        {
            SicmAlertas = new HashSet<SICM_ALERTAS>();
        }

        public int CodigoTipoAlerta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SICM_ALERTAS> SicmAlertas { get; set; }
    }
}
