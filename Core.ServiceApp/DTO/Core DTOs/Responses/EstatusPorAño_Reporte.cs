using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Responses
{
    public class EstatusPorAño_Reporte
    {
        public int codigoEstatus { get; set; }
        public string Estatus { get; set; }
        public List<int> anios { get; set; }
        public List<int> CantidadPorAnio { get; set; }
    }
}
