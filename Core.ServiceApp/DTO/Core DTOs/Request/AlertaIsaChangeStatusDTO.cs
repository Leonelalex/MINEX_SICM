using System.ComponentModel.DataAnnotations;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class AlertaIsaChangeStatusDTO
    {
        public string usuario { get; set; }
        public string institucion { get; set; }
        public string motivo { get; set; }
        public string aplicacion { get; set; }
        public alerta alerta {get; set;}

    }
    public class alerta
    {
        public int id { get; set; }
        public string numero_alerta { get; set; }
        public string cc { get; set; }
        public int ano { get; set; }
        public int  id_fiscalia { get; set; }
        public int folio { get; set; }
        public string id_cloud_oficio_desactivacion_firmado { get; set; }
    }
}
