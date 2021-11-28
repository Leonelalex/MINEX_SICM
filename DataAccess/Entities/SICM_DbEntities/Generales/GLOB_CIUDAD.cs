using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.SICM_DbEntities.Generales
{
    public class GLOB_CIUDAD
    { 
        public GLOB_CIUDAD()
        {
            SicmAlertas = new HashSet<SICM_ALERTAS>();
        }
        [Key]
        public int CODIGO_CIUDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public int HUSO_HORARIO { get; set; }
        public int CODIGO_DIVISION { get; set; }

        public virtual GLOB_DIVISION CodigoDivisionNavigation { get; set; }
        public virtual ICollection<SICM_ALERTAS> SicmAlertas { get; set; }
      //  public virtual ICollection<SicmAvistamientos> SicmAvistamientos { get; set; }

    }
}
