using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.SICM_DbEntities.Generales
{
    public class GLOB_DIVISION
    {
        public GLOB_DIVISION()
        {
            GLOB_CIUDAD = new HashSet<GLOB_CIUDAD>();
        }

        [Key]
        public int CODIGO_DIVISION { get; set; }
        public string DESCRIPCION { get; set; }
        public int CODIGO_PAIS { get; set; }

        public virtual GLOB_PAIS CodigoPaisNavigation { get; set; }
        public virtual ICollection<GLOB_CIUDAD> GLOB_CIUDAD { get; set; }
    }
}
