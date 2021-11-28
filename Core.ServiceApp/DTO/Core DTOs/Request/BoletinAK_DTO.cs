using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class BoletinAK_DTO
    {
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string tercerNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public int edad { get; set; }
        public DateTime fechaHora { get; set; }
        public string foto { get; set; }
        public int colorCabello { get; set; }
        public int tipoCabello { get; set; }
        public int tamanioCabello { get; set; }
        public int colorOjos { get; set; }
        public int complexion { get; set; }
        public int tez { get; set; }
        public string vestimenta { get; set; }
        public int estatura { get; set; }
        public int genero { get; set; }
        public string seniasParticulares { get; set; }
        public string nombrePadre { get; set; }
        public string nombreMadre { get; set; }
        public string responsable { get; set; }
        public string alias { get; set; }
        public string observaciones { get; set; }
        public string notas { get; set; }
        public string boletin { get; set; }
    }
}
