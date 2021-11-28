using System;
using System.Collections.Generic;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class AlertaAlbaKeneth_DTO
    {

        public string codigoAlerta { get; set; }

        public string direccion { get; set; }

        public int codigoMunicipio { get; set; }

        public string codigoOficio { get; set; }

        public string oficio { get; set; }

        public string observaciones { get; set; }

        public string correosAdicionales { get; set; }

        public string numeroCaso { get; set; }

        public bool difusionInternacional { get; set; }
        public string denunciante { get; set; }

        public List<int> misiones { get; set; }

        public List<BoletinAK_DTO> boletines { get; set; }

    }


}
