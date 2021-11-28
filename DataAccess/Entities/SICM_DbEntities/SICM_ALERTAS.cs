using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.SICM_DbEntities
{
    [Index(nameof(codigoAlerta), nameof(tipoAlerta))]
    public class SICM_ALERTAS : AuditEntityBase
    {
        public SICM_ALERTAS()
        {
            SicmAlertaMisiones = new HashSet<SICM_ALERTA_MISIONES>();
            SicmAvistamientos = new HashSet<SICM_AVISTAMIENTOS>();
            SicmBitacoraAlerta = new HashSet<SICM_BITACORA_ALERTA>();
            SicmBoletines = new HashSet<SICM_BOLETINES>();
        }

        [Key]
        public int codigo { get; set; }

        public string codigoAlerta { get; set; }

        public int codigoMunicipio { get; set; }
        public string direccion { get; set; }

        [StringLength(1000, ErrorMessage = "El campo de observaciones es demaciado largo")]
        public string observaciones { get; set; }

        public int estadoAlerta { get; set; }

        public DateTime fechaActivacion { get; set; }

        public DateTime? fechaDesactivacion { get; set; }

        public string codigoUsuario { get; set; }

        public string codigoOficio { get; set; }

        public string oficio { get; set; }

        public int tipoAlerta { get; set; }

        public string correosAdicionales { get; set; }

        public string numeroCaso { get; set; }

        public bool? difusionInternacional { get; set; }
        public string denunciante { get; set; }

        public virtual GLOB_CIUDAD CodigoMunicipioNavigation { get; set; }

        public virtual SICM_TIPO_ALERTA CodigoTipoAlertaNavigation { get; set; }

        public virtual SICM_ESTADOS_ALERTA EstadoAlertaNavigation { get; set; }

        public virtual ICollection<SICM_ALERTA_MISIONES> SicmAlertaMisiones { get; set; }

        public virtual ICollection<SICM_AVISTAMIENTOS> SicmAvistamientos { get; set; }

        public virtual ICollection<SICM_BITACORA_ALERTA> SicmBitacoraAlerta { get; set; }

        public virtual ICollection<SICM_BOLETINES> SicmBoletines { get; set; }


    }
}
