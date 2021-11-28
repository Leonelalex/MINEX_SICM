using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.SICM_DbEntities
{
    public class SICM_BOLETINES
    {
        [Key]
        public int codigoBoletin { get; set; }
        public int codigoAlerta { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string tercerNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string cui { get; set; }
        public int edad { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public DateTime? fechaHora { get; set; }
        public string foto { get; set; }
        public int colorCabello { get; set; }
        public int tipoCabello { get; set; }
        public int tamanioCabello { get; set; }
        public int tipoCeja { get; set; }
        public int colorOjos { get; set; }
        public int tipoNariz { get; set; }
        public int complexion { get; set; }
        public int tez { get; set; }
        public string notas { get; set; }
        public string vestimenta { get; set; }
        public int estatura { get; set; }
        public int genero { get; set; }
        public string seniasParticulares { get; set; }
        public int nacionalidad { get; set; }
        public int codigoEstatus { get; set; }
        public int codigoSituacion { get; set; }
        public string boletin { get; set; }
        public string apellidoCasada { get; set; }
        public string nombrePadre { get; set; }
        public string nombreMadre { get; set; }
        public string responsable { get; set; }
        public string alias { get; set; }
        public string observaciones { get; set; }
        public virtual SICM_ALERTAS CodigoAlertaNavigation { get; set; }


    }
}
