using DataAccess.Entities.SICM_DbEntities.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class GLOB_DIVISIONConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<GLOB_DIVISION> entity)
        {
           /* entity.ToTable("GLOB_DIVISION", "glob");
            entity.HasKey(e => e.CODIGO_DIVISION);

            entity.Property(e => e.DESCRIPCION);

            entity.Property(e => e.CODIGO_PAIS);*/

            entity.HasKey(e => e.CODIGO_DIVISION)
                   .HasName("PK__GLOB_DIVISION__1CF15040");

            entity.ToTable("GLOB_DIVISION", "glob");

            entity.HasComment("Almacena las divisiones");

            entity.Property(e => e.CODIGO_DIVISION)
                .HasColumnName("CODIGO_DIVISION")
                .HasComment("Codigo correlativo unico para cada registro");

            entity.Property(e => e.CODIGO_PAIS)
                .HasColumnName("CODIGO_PAIS")
                .HasComment("Almacena el pais");

            entity.Property(e => e.DESCRIPCION)
                .IsRequired()
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Almacena la descripcion");

            entity.HasOne(d => d.CodigoPaisNavigation)
                .WithMany(p => p.GLOB_DIVISION)
                .HasForeignKey(d => d.CODIGO_PAIS)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GLOB_DIVISION__FK_GLOB_PAIS");
        }
    }
}
