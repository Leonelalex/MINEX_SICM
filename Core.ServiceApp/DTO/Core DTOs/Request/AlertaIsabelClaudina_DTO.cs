using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.ServiceApp.DTO.Core_DTOs.Request
{
    public class AlertaIsabelClaudina_DTO
    {
        public string usuario { get; set; }
        public string institucion { get; set; }
        public string motivo { get; set; }
        public string aplicacion { get; set; }
        public alertaIC alerta { get; set; }
    }

    public class alertaIC
    {
        public int id { get; set; }
        public string numero_alerta { get; set; }
        public string cc { get; set; }
        public int ano { get; set; }
        public int id_fiscalia { get; set; }
        public int folio { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string otroNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string apellidoCasada { get; set; }
        public string desaparicion { get; set; }
        public string id_cloud_boletin { get; set; }
        public bool flag_activacion_internacional { get; set; }
        public string id_cloud_oficio_internacional_firmado { get; set; }
        public string id_cloud_oficio_activacion_firmado { get; set; }
        public double estatura { get; set; }
        public int edad { get; set; }
        public int id_tipo_color_piel { get; set; }
        public int id_complexion { get; set; }
        public int id_tipo_ceja { get; set; }
        public int id_tipo_nariz { get; set; }
        public int id_tipo_ojo { get; set; }
        public string vestimenta { get; set; }
        public string observacion { get; set; }
        public int id_departamento { get; set; }
        public int id_municipio { get; set; }
        public int id_color_cabello { get; set; }
        public int id_longitud_cabello { get; set; }
        public int id_tipo_cabello { get; set; }
        public string cui { get; set; }
        public direccionMP direccion { get; set; }
    }

    public class direccionMP {
        public int id_direccion { get; set; }
        public string numero_calle { get; set; }
        public string numero_avenida { get; set; }
        public string numero_casa { get; set; }
        public int id_zona { get; set; }
        public string colonia { get; set; }
        public string descripcion { get; set; }
        public int id_municipio { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public int id_apartado { get; set; }
        public int id_departamento { get; set; }
        public int id_pais { get; set; }
        public string observacion { get; set; }
    }

}

