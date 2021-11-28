

using System;

namespace DataAccess.Entities.SICM_DbEntities.Generales
{
    public partial class GLOB_TIPO_MISION
    {
        public int CODIGO_TIPO_MISION { get; set; }
        public string NOMBRE_TIPO_MISION { get; set; }
        public string DESCRIPCION { get; set; }
        public int ELIMINADO { get; set; }
        public DateTime FECHA_ELIMINACION { get; set; }
        public string USUARIO_ELIMINA { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
        public string USUARIO_MODIFICA { get; set; }
        public DateTime FECHA_REGISTRO { get; set; }
        public string USUARIO_REGISTRA { get; set; }
    }
}
