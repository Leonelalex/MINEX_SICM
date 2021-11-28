using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_SITUACION_ALERTAConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_SITUACION_ALERTA> entity)
        {
            entity.HasKey(e => e.Codigo).HasName("PK_SICM_SITUACION_ALERTA");

            entity.ToTable("SICM_SITUACION_ALERTA");

            entity.Property(e => e.Codigo).HasColumnName("CODIGO");

            entity.Property(e => e.Activo).HasColumnName("ACTIVO");

            entity.Property(e => e.Descripcion)
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("NOMBRE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.isAK).HasColumnName("IS_AK");

            entity.Property(e => e.isIC).HasColumnName("IS_IC");

        }
    }
}
