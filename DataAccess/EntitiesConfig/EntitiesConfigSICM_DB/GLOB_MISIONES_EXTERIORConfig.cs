using DataAccess.Entities.SICM_DbEntities.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class GLOB_MISIONES_EXTERIORConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<GLOB_MISIONES_EXTERIOR> entity)
        {
            entity.HasKey(e => e.ID_MISION_EXTERIOR);

            entity.ToTable("GLOB_MISIONES_EXTERIOR", "glob");

            entity.Property(e => e.CIRCUNSCRIPCION_SECCION_CONSULAR)
                .HasColumnName("CIRCUNSCRIPCION_SECCION_CONSULAR")
                .HasMaxLength(500);

            entity.Property(e => e.CODIGO_CIUDAD).HasColumnName("CODIGO_CIUDAD");

            entity.Property(e => e.CODIGO_DEPARTAMENTO).HasColumnName("CODIGO_DEPARTAMENTO");

            entity.Property(e => e.CODIGO_PAIS).HasColumnName("CODIGO_PAIS");

            entity.Property(e => e.CODIGO_TIPO_MISION).HasColumnName("CODIGO_TIPO_MISION");

            entity.Property(e => e.CORREO_ELECTRONICO)
                .HasColumnName("CORREO_ELECTRONICO")
                .HasMaxLength(150);

            entity.Property(e => e.DIRECCION)
                .HasColumnName("DIRECCION")
                .HasMaxLength(250);

            entity.Property(e => e.FAX)
                .HasColumnName("FAX")
                .HasMaxLength(150);

            entity.Property(e => e.FECHA_ACREDITACION)
                .HasColumnName("FECHA_ACREDITACION")
                .HasColumnType("datetime");

            entity.Property(e => e.FIESTA_NACIONAL)
                .HasColumnName("FIESTA_NACIONAL")
                .HasMaxLength(250);

            entity.Property(e => e.HORARIO)
                .HasColumnName("HORARIO")
                .HasMaxLength(150);

            entity.Property(e => e.ID_MISION_EXTERIOR).HasColumnName("ID_MISION_EXTERIOR");

            entity.Property(e => e.MONEDA).HasColumnName("MONEDA");

            entity.Property(e => e.NOMBRE_MISION)
                .IsRequired()
                .HasColumnName("NOMBRE_MISION")
                .HasMaxLength(250);

            entity.Property(e => e.NOMBRE_MISION_TRANSFERENCIA)
                .IsRequired()
                .HasColumnName("NOMBRE_MISION_TRANSFERENCIA")
                .HasMaxLength(250);

            entity.Property(e => e.NOTAS)
                .HasColumnName("NOTAS")
                .HasMaxLength(250);

            entity.Property(e => e.SITIO_WEB)
                .HasColumnName("SITIO_WEB")
                .HasMaxLength(250);

            entity.Property(e => e.TELEFONO)
                .HasColumnName("TELEFONO")
                .HasMaxLength(150);

            entity.Property(e => e.ZIP)
                .HasColumnName("ZIP")
                .HasMaxLength(10)
                .IsFixedLength();

            entity.Property(e => e.PRIORIDAD)
                .HasColumnName("PRIORIDAD");

            entity.HasOne(d => d.CodigoTipoMisionNavigation)
                .WithMany()
                .HasForeignKey(d => d.CODIGO_TIPO_MISION)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GLOB_MISIONES_EXTERIOR_GLOB_TIPO_MISION");

            entity.HasOne(d => d.IdMisionExteriorNavigation)
                .WithMany()
                .HasForeignKey(d => d.ID_MISION_EXTERIOR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GLOB_MISIONES_EXTERIOR_SICM_ALERTA_MISIONES");
        }
    }
}
