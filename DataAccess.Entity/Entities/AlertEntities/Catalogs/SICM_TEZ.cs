
using System.ComponentModel.DataAnnotations;


namespace DataAccess.Entities.AlertEntities
{
    public partial class SICM_TEZ
    {
        public int CODIGO { get; set; }

        [MaxLength(50)]
        public string NOMBRE { get; set; }

    }
}
