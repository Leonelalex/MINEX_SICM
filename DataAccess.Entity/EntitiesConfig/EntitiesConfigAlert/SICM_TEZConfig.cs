using DataAccess.Entities.AlertEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfig.EntityConfigAlert
{
    public class SICM_TEZConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_TEZ> entity)
        {

            entity.HasKey(e => e.CODIGO);

            entity.Property(e => e.NOMBRE).HasMaxLength(50);
        }
    }
}
