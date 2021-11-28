using DataAccess.Entities.SICM_DbEntities.Catalogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_COMPLEXIONConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_COMPLEXION> entity)
        {

            entity.HasKey(e => e.Codigo)
                    .HasName("PK__SICM_COM__CC87E1279C4101DE");

            entity.ToTable("SICM_COMPLEXION");

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
