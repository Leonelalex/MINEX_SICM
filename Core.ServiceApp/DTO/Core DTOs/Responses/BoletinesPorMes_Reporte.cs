using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Responses
{
    class BoletinesPorMes_Reporte
    {
        public int codigo { get; set; }
        public string mes { get; set; }
        public int hombres { get; set; }
        public int mujeres { get; set; }
    }
}
