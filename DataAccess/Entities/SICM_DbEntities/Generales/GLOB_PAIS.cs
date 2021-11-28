using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.SICM_DbEntities.Generales
{
    public partial class GLOB_PAIS
    {
        public GLOB_PAIS()
        {
            GLOB_DIVISION = new HashSet<GLOB_DIVISION>();
        }
        [Key]
        public int CODIGO_PAIS { get; set; }
        public string DESCRIPCION   { get; set; }
        public int HUSO_HORARIO { get; set; }
        public string CODIGO_ISO_ALPHA3 { get; set; }
        public int CODIGO_REGION { get; set; }
        public int CODIGO_MONEDA { get; set; }
        public DateTime? FECHA_INICIO_RELACIONES { get; set; }
        public string NACIONALIDAD { get; set; }
        public string IMG_BANDERA { get; set; }
        public string CATEGORIA_ORDINARIA { get; set; }
        public string CATEGORIA_OFICIAL { get; set; }
        public string IMG_MAPA { get; set; }
        public string HTML_RELOJ { get; set; }
        public int PRIORIDAD { get; set; }

        public virtual ICollection<GLOB_DIVISION> GLOB_DIVISION { get; set; }
    }
}
