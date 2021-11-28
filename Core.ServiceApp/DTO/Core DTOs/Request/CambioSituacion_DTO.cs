using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class CambioSituacion_DTO
    {
        public int estatus { get; set; }
        public int situacion { get; set; }
        public string comentarios { get; set; }
        public string adjuntos { get; set; }
        public string correosAdicionales { get; set; }
    }
}
