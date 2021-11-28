using DataAccess.Entities.SICM_DbEntities.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class GLOB_GENEROConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<GLOB_GENERO> entity)
        {
            entity.HasKey(e => e.CODIGO_GENERO)
                   .HasName("PK__GLOB_GENERO__153B1FDF");

            entity.ToTable("GLOB_GENERO", "glob");

            entity.HasComment("Almacena los generos");

            entity.Property(e => e.CODIGO_GENERO)
                .HasColumnName("CODIGO_GENERO")
                .HasComment("Codigo correlativo unico para cada registro");

            entity.Property(e => e.DESCRIPCION)
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Almacena la descripcion");

        }
    }
}
