using System;

namespace DataAccess.Entities.SICM_DbEntities.Generales
{
    public partial class GLOB_MISIONES_EXTERIOR
    {
        public int ID_MISION_EXTERIOR {get; set;}
        public string  NOMBRE_MISION { get; set; }
        public string NOMBRE_MISION_TRANSFERENCIA { get; set; }
        public int CODIGO_TIPO_MISION { get; set; }
        public int CODIGO_PAIS { get; set; }
        public int CODIGO_CIUDAD { get; set; }
        public int CODIGO_DEPARTAMENTO { get; set; }
        public int DIRECCION { get; set; }
        public string ZIP { get; set; }
        public string TELEFONO { get; set; }
        public string FAX { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string SITIO_WEB { get; set; }
        public string CIRCUNSCRIPCION_SECCION_CONSULAR { get; set; }
        public DateTime FECHA_ACREDITACION { get; set; }
        public string HORARIO { get; set; }
        public string FIESTA_NACIONAL { get; set; }
        public int MONEDA { get; set; }
        public string NOTAS { get; set; }
        public int PRIORIDAD { get; set; }


        public virtual GLOB_TIPO_MISION CodigoTipoMisionNavigation { get; set; }
        public virtual SICM_ALERTA_MISIONES IdMisionExteriorNavigation { get; set; }

    }
}
