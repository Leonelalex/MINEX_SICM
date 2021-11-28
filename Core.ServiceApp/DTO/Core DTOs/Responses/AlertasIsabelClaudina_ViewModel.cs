using DataAccess.Entities.SICM_DbEntities;
using System;
using System.Collections.Generic;

namespace Core.ServiceApp.DTO.Core_DTOs.Responses
{
    public class AlertasIsabelClaudina_ViewModel
    {
        public int codigo { get; set; }

        public string codigoAlerta { get; set; }

        public int codigoMunicipio { get; set; }
        public string direccion { get; set; }

        public string observaciones { get; set; }

        public int estadoAlerta { get; set; }

        public DateTime? fechaActivacion { get; set; }

        public DateTime? fechaDesactivacion { get; set; }

        public string codigoUsuario { get; set; }

        public string codigoOficio { get; set; }

        public string oficio { get; set; }

        public int tipoAlerta { get; set; }

        public int CodigoSituacion { get; set; }

        public string correosAdicionales { get; set; }

        public ICollection<SICM_BOLETINES> SicmBoletines { get; set; }

        public ICollection<SICM_ALERTA_MISIONES> SicmAlertaMisiones { get; set; }
    }
}
