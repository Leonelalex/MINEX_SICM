using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_AVISTAMIENTOSConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_AVISTAMIENTOS> entity)
        {
            entity.HasKey(e => e.CodigoAvistamiento)
                    .HasName("PK_AVISTAMIENTOS");

            entity.ToTable("SICM_AVISTAMIENTOS");

            entity.Property(e => e.CodigoAvistamiento).HasColumnName("CODIGO_AVISTAMIENTO");

            entity.Property(e => e.ActualizadoFecha).HasColumnType("datetime");

            entity.Property(e => e.ActualizadoPor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Anonimo).HasColumnName("ANONIMO");

            entity.Property(e => e.CodigoAlerta).HasColumnName("CODIGO_ALERTA");

            entity.Property(e => e.CodigoMunicipio).HasColumnName("CODIGO_MUNICIPIO");

            entity.Property(e => e.CodigoUsuario)
                .IsRequired()
                .HasColumnName("CODIGO_USUARIO")
                .HasMaxLength(128)
                .IsUnicode(false);

            entity.Property(e => e.Correo)
                .HasColumnName("CORREO")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreadoFecha).HasColumnType("datetime");

            entity.Property(e => e.CreadoPor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Direccion)
                .HasColumnName("DIRECCION")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.EliminadoFecha).HasColumnType("datetime");

            entity.Property(e => e.EliminadoPor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FechaHora)
                .HasColumnName("FECHA_HORA")
                .HasColumnType("datetime");

            entity.Property(e => e.Motivo)
                .IsRequired()
                .HasColumnName("MOTIVO")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Nombre)
                .HasColumnName("NOMBRE")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Observaciones)
                .HasColumnName("OBSERVACIONES")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Telefono)
                .HasColumnName("TELEFONO")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoAlertaNavigation)
                .WithMany(p => p.SicmAvistamientos)
                .HasForeignKey(d => d.CodigoAlerta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SICM_AVISTAMIENTOS_SICM_ALERTAS");

            /*entity.HasOne(d => d.CodigoMunicipioNavigation)
                .WithMany(p => p.SicmAvistamientos)
                .HasForeignKey(d => d.CodigoMunicipio)
                .HasConstraintName("FK_SICM_AVISTAMIENTOS_GLOB_CIUDAD");*/
        }
    }
}
