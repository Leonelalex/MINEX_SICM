using DataAccess.Entities.SICM_DbEntities.Catalogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_TIPO_CEJAConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_TIPO_CEJA> entity)
        {
            entity.HasKey(e => e.Codigo)
                .HasName("PK__SICM_TIP__CC87E12728DBFC04");

            entity.ToTable("SICM_TIPO_CEJA");

            entity.Property(e => e.Codigo)
                .HasColumnName("CODIGO")
                .ValueGeneratedNever();

            entity.Property(e => e.Nombre)
                .HasColumnName("NOMBRE")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
