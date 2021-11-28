namespace DataAccess.Entities.SICM_DbEntities
{
    public partial class SICM_ALERTA_MISIONES
    {
        public int CodigoAlertaMision { get; set; }
        public int CodigoAlerta { get; set; }
        public int CodigoMision { get; set; }

        public virtual SICM_ALERTAS CodigoAlertaNavigation { get; set; }
    }
}
