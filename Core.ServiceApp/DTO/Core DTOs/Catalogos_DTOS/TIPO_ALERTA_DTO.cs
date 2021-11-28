using System.ComponentModel.DataAnnotations;

namespace Core.ServiceApp.DTO.Core_DTOs
{
    public class TIPO_ALERTA_DTO
    {
        [Key]
        public int CODIGO_TIPO_ALERTA { get; set; }

        public string NOMBRE { get; set; }

        public string DESCRIPCION { get; set; }
    }
}
