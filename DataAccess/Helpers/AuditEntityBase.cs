using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Helpers
{
    public abstract class AuditEntityBase
    {
        public DateTime CreadoFecha { get; set; }

        [StringLength(128)]
        public string CreadoPor { get; set; }

        public DateTime ActualizadoFecha { get; set; }

        [StringLength(128)]
        public string ActualizadoPor { get; set; }

        public DateTime EliminadoFecha { get; set; }

        [StringLength(128)]
        public string EliminadoPor { get; set; }

        public bool IsDeleted { get; set; }
    }
}
