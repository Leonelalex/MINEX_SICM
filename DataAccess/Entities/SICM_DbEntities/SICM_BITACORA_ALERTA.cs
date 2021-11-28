using DataAccess.Helpers;
using System;

namespace DataAccess.Entities.SICM_DbEntities
{
    public partial class SICM_BITACORA_ALERTA
    {
        public int Codigo { get; set; }
        public int codigoAlerta { get; set; }
        public int codigoEstado { get; set; }
        public int codigoAccion { get; set; }
        public DateTime? fecha { get; set; }
        public string observaciones { get; set; }
        public string adjunto { get; set; }
        public int? codigoAccionNotificacion { get; set; }
        public string usuario { get; set; }
        public string destinatarios { get; set; }
        public string pais { get; set; }

        public virtual SICM_ACCIONES_ALERTA CodigoAccionNavigation { get; set; }
        public virtual SICM_ALERTAS CodigoAlertaNavigation { get; set; }
        public virtual SICM_ESTADOS_ALERTA CodigoEstadoNavigation { get; set; }

    }
}
