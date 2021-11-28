using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_BITACORA_ALERTAConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_BITACORA_ALERTA> entity)
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("SICM_BITACORA_ALERTA");

            entity.Property(e => e.Codigo)
                .HasColumnName("CODIGO");

            entity.Property(e => e.adjunto)
                .HasColumnName("ADJUNTO")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.codigoAccion).HasColumnName("CODIGO_ACCION");

            entity.Property(e => e.codigoAlerta).HasColumnName("CODIGO_ALERTA");

            entity.Property(e => e.codigoEstado).HasColumnName("CODIGO_ESTADO");

            entity.Property(e => e.fecha)
                .HasColumnName("FECHA")
                .HasColumnType("datetime");

            entity.Property(e => e.observaciones)
                .HasColumnName("OBSERVACIONES")
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.Property(e => e.usuario).HasColumnName("USUARIO");

            entity.Property(e => e.destinatarios).HasColumnName("DESTINATARIOS");

            entity.Property(e => e.codigoAccionNotificacion).HasColumnName("CODIGO_ACCION_NOTIFICACION");

            entity.Property(e => e.pais).HasColumnName("PAIS");

            entity.HasOne(d => d.CodigoAccionNavigation)
                .WithMany(p => p.SicmBitacoraAlerta)
                .HasForeignKey(d => d.codigoAccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SICM_BITACORA_ALERTA_SCIM_ACCIONES_ALERTA");

            entity.HasOne(d => d.CodigoAlertaNavigation)
                .WithMany(p => p.SicmBitacoraAlerta)
                .HasForeignKey(d => d.codigoAlerta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SICM_BITACORA_ALERTA_SICM_ALERTAS");

            entity.HasOne(d => d.CodigoEstadoNavigation)
                .WithMany(p => p.SicmBitacoraAlerta)
                .HasForeignKey(d => d.codigoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SICM_BITACORA_ALERTA_SICM_ESTADOS_ALERTA");

        }
    }
}
