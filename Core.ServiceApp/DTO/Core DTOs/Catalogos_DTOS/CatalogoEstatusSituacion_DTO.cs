using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceApp.DTO.Core_DTOs.Catalogos_DTOS
{
    public class CatalogoEstatusSituacion_DTO
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        public bool isAK { get; set; }
        public bool isIC { get; set; }
    }
}
