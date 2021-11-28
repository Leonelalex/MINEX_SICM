using DataAccess.Entities.SICM_DbEntities.Catalogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_TIPO_ALERTAConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_TIPO_ALERTA> entity)
        {
            entity.HasKey(e => e.CodigoTipoAlerta);

            entity.ToTable("SICM_TIPO_ALERTA");

            entity.Property(e => e.CodigoTipoAlerta)
                .HasColumnName("CODIGO_TIPO_ALERTA")
                .ValueGeneratedNever();

            entity.Property(e => e.Descripcion)
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Nombre)
                .HasColumnName("NOMBRE")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
