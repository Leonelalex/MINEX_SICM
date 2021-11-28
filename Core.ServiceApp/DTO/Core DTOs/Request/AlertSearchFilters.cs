using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class AlertSearchFilters
    {
        public AlertSearchFilters()
        {
            codigoAlerta = null;
            estado = 0;
            numeroCaso = null;
            codigoOficio = null;
            fechaActivacionIni = DateTime.MinValue;
            fechaActivacionFin = DateTime.MinValue;
            fechaDesactivacionIni = DateTime.MinValue;
            fechaDesactivacionFin = DateTime.MinValue;
        }
        public string codigoAlerta { get; set; }
        public int estado { get; set; }
        public string numeroCaso { get; set; }
        public string codigoOficio { get; set; }
        public DateTime fechaActivacionIni { get; set; }
        public DateTime fechaActivacionFin { get; set; }
        public DateTime fechaDesactivacionIni { get; set; }
        public DateTime fechaDesactivacionFin { get; set; }
    }
}
