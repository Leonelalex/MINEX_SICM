using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class NotificarAutoridades_DTO
    {
        public string comentarios { get; set; }
        public string adjuntos { get; set; }
        public string correosAdicionales { get; set; }
        public List<int> acciones { get; set; }
    }
}
