using System;

namespace Core.ServiceApp.DTO.Core_DTOs.Responses
{
    public class AlertaIsabelClaudina_ViewModel // : respuesta_ViewModel
    {
        public RespuestaMP_ViewModel respuesta { get; set; }
        public int idError { get; set; }
        public string message { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime responseTime { get; set; }

    }
}
