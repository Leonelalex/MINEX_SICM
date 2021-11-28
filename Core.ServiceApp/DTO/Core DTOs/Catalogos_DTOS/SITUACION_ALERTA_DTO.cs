using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Catalogos_DTOS
{
    public class SITUACION_ALERTA_DTO
    {
        [Key]
        public int CODIGO { get; set; }

        public string NOMBRE { get; set; }
    }
}
