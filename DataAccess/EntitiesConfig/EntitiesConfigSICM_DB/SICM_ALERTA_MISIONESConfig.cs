using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_ALERTA_MISIONESConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_ALERTA_MISIONES> entity)
        {
            entity.HasKey(e => e.CodigoAlertaMision)
    .HasName("PK_MDCG_TIPO_MISION");

            entity.ToTable("SICM_ALERTA_MISIONES");

            entity.Property(e => e.CodigoAlertaMision).HasColumnName("CODIGO_ALERTA_MISION");

            entity.Property(e => e.CodigoAlerta).HasColumnName("CODIGO_ALERTA");

            entity.Property(e => e.CodigoMision).HasColumnName("CODIGO_MISION");

            entity.HasOne(d => d.CodigoAlertaNavigation)
                .WithMany(p => p.SicmAlertaMisiones)
                .HasForeignKey(d => d.CodigoAlerta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SICM_ALERTA_MISIONES_SICM_ALERTAS");
        }
    }
}
