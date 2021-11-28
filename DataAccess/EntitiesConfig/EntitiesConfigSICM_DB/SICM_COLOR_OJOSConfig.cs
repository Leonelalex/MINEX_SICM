using DataAccess.Entities.SICM_DbEntities.Catalogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_COLOR_OJOSConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_COLOR_OJOS> entity)
        {

            entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_COL__CC87E127B0B7F049");

            entity.ToTable("SICM_COLOR_OJOS");

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
