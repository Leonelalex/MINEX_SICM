using System.Collections.Generic;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class Activar_IC_DTO
    {
        public bool difusionInternacional { get; set; }
        public List<int> misiones { get; set; }
        public string correosAdicionales { get; set; }
    }
}
