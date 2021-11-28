using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs
{
    public class GLOB_PAIS_DTO
    {
        [Key]
        public int CODIGO_PAIS { get; set; }

        public string DESCRIPCION { get; set; }
    }
}
