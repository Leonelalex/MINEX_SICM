using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_ALERTASConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_ALERTAS> entity)
        {
            entity.ToTable("SICM_ALERTAS");

            entity.HasKey(e => e.codigo)
                                .HasName("PK_SICM_ALERTAS_1");

            entity.Property(e => e.codigo).HasColumnName("CODIGO");

            entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.codigoAlerta)
                .IsRequired()
                .HasColumnName("CODIGO_ALERTA")
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.tipoAlerta).HasColumnName("CODIGO_TIPO_ALERTA");

            entity.Property(e => e.codigoUsuario)
                .HasColumnName("CODIGO_USUARIO")
                .HasMaxLength(128)
                .IsUnicode(false);

            entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

            entity.Property(e => e.CreadoPor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.direccion)
                .HasColumnName("DIRECCION")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.codigoMunicipio)
                .HasColumnName("CODIGO_MUNICIPIO");

            entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.estadoAlerta).HasColumnName("ESTADO_ALERTA");

            entity.Property(e => e.fechaActivacion)
                .HasColumnName("FECHA_ACTIVACION")
                .HasColumnType("date");

            entity.Property(e => e.fechaDesactivacion)
                .HasColumnName("FECHA_DESACTIVACION");

            entity.Property(e => e.observaciones)
                .HasColumnName("OBSERVACIONES")
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.oficio)
                .HasColumnName("OFICIO")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.correosAdicionales)
                .HasColumnName("CORREOS_ADICIONALES")
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.numeroCaso)
                .HasColumnName("NUMERO_CASO");

            entity.Property(e => e.denunciante).HasColumnName("DENUNCIANTE");

            entity.Property(e => e.difusionInternacional)
                .HasColumnName("DIFUSION_INTERNACIONAL");

            entity.HasOne(d => d.CodigoMunicipioNavigation)
                .WithMany(p => p.SicmAlertas)
                .HasForeignKey(d => d.codigoMunicipio)
                .HasConstraintName("FK_SICM_ALERTAS_GLOB_CIUDAD");

            entity.HasOne(d => d.CodigoTipoAlertaNavigation)
                .WithMany(p => p.SicmAlertas)
                .HasForeignKey(d => d.tipoAlerta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SICM_ALERTAS_SICM_TIPO_ALERTA");

            entity.HasOne(d => d.EstadoAlertaNavigation)
                .WithMany(p => p.SicmAlertas)
                .HasForeignKey(d => d.estadoAlerta)
                .HasConstraintName("FK_SICM_ALERTAS_SICM_ESTADOS_ALERTA");


            entity.Property(e => e.codigoOficio).HasColumnName("CODIGO_OFICIO");

        }
    }
}
