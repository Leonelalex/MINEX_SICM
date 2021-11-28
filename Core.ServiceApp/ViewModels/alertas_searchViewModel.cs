using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ServiceApp.ViewModels
{
    public partial class alertas_searchViewModel
    {
        public int codigo { get; set; }
        public string codigoAlerta { get; set; }
        public int codigoMunicipio { get; set; }
        public string direccion { get; set; }
        public string observaciones { get; set; }
        public int estadoAlerta { get; set; }
        public DateTime? fechaActivacion { get; set; }
        public DateTime? fechaDesactivacion { get; set; }
        public string codigoOficio { get; set; }
        public string oficio { get; set; }
        public int tipoAlerta { get; set; }
        public int codigoSituacion { get; set; }
        public boletines_searchViewModel boletines { get; set; }
    }

    public partial class boletines_searchViewModel { 
        public int codigoBoletin { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string tercerNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string apellidoCasada { get; set; }
        public string cui { get; set; }
        public int edad { get; set; }
        public string fechaNacimiento { get; set; }
        public string fechaHora { get; set; }
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

    }
}
