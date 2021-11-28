using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.SICM_DbEntities.Generales
{
    public class GLOB_GENERO
    {
        [Key]
        public int CODIGO_GENERO { get; set; }
        public string DESCRIPCION { get; set; }


    }
}
