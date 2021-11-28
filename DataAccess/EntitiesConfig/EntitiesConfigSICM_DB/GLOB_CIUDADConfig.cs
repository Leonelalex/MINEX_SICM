using DataAccess.Entities.SICM_DbEntities.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class GLOB_CIUDADConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<GLOB_CIUDAD> entity)
        {
            entity.HasKey(e => e.CODIGO_CIUDAD)
                .HasName("PK__GLOB_CIUDAD__30F848ED");

            entity.ToTable("GLOB_CIUDAD", "glob");

            entity.HasComment("Almacena las ciudades");

            entity.Property(e => e.CODIGO_CIUDAD)
                .HasColumnName("CODIGO_CIUDAD")
                .HasComment("Codigo correlativo unico para cada registro");

            entity.Property(e => e.CODIGO_DIVISION)
                .HasColumnName("CODIGO_DIVISION")
                .HasComment("Almacena la division");

            entity.Property(e => e.DESCRIPCION)
                .IsRequired()
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Almacena la descripcion");

            entity.Property(e => e.HUSO_HORARIO)
                .HasColumnName("HUSO_HORARIO")
                .HasComment("Almacena el huso del horario");

            entity.HasOne(d => d.CodigoDivisionNavigation)
                .WithMany(p => p.GLOB_CIUDAD)
                .HasForeignKey(d => d.CODIGO_DIVISION)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GLOB_CIUDAD__GLOB_DIVISION");
        }
    }
}
