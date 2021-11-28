using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class UltimasAcciones_DTO
    {
        public int tipoAlerta { get; set; }
        public DateTime fechaInicio { get; set;  }
        public DateTime fechaFin { get; set; }
    }
}
