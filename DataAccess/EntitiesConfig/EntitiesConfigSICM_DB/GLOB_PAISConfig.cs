using DataAccess.Entities.SICM_DbEntities.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class GLOB_PAISConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<GLOB_PAIS> entity)
        {

            entity.HasKey(e => e.CODIGO_PAIS)
                    .HasName("PK__GLOB_PAIS__2E1BDC42");

            entity.ToTable("GLOB_PAIS", "glob");

            entity.HasComment("Almacena los paises");

            entity.Property(e => e.CODIGO_PAIS)
                .HasColumnName("CODIGO_PAIS")
                .HasComment("Codigo correlativo unico para cada pais");

            entity.Property(e => e.CATEGORIA_OFICIAL)
                .IsRequired()
                .HasColumnName("CATEGORIA_OFICIAL")
                .HasMaxLength(1)
                .HasComment("Almacena la categoria oficial");

            entity.Property(e => e.CATEGORIA_ORDINARIA)
                .IsRequired()
                .HasColumnName("CATEGORIA_ORDINARIA")
                .HasMaxLength(1)
                .HasComment("Almacena la categoria ordinaria");

            entity.Property(e => e.CODIGO_ISO_ALPHA3)
                .IsRequired()
                .HasColumnName("CODIGO_ISO_ALPHA3")
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Almacena el codigo de iso");

            entity.Property(e => e.CODIGO_MONEDA)
                .HasColumnName("CODIGO_MONEDA")
                .HasComment("Almacena el codigo de moneda");

            entity.Property(e => e.CODIGO_REGION)
                .HasColumnName("CODIGO_REGION")
                .HasComment("Almacena el codigo de region");

            entity.Property(e => e.DESCRIPCION)
                .IsRequired()
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Almacena la descripcion");

            entity.Property(e => e.FECHA_INICIO_RELACIONES)
                .HasColumnName("FECHA_INICIO_RELACIONES")
                .HasColumnType("datetime")
                .HasComment("Almacena la fecha de inicio de relaciones");

            entity.Property(e => e.HTML_RELOJ)
                .HasColumnName("HTML_RELOJ")
                .HasColumnType("text");

            entity.Property(e => e.HUSO_HORARIO)
                .HasColumnName("HUSO_HORARIO")
                .HasComment("Almacena el huso de horari");

            entity.Property(e => e.IMG_BANDERA)
                .HasColumnName("IMG_BANDERA")
                .HasColumnType("image")
                .HasComment("Almacena la bandera");

            entity.Property(e => e.IMG_MAPA)
                .HasColumnName("IMG_MAPA")
                .HasColumnType("image");

            entity.Property(e => e.NACIONALIDAD)
                .HasColumnName("NACIONALIDAD")
                .HasMaxLength(250)
                .HasComment("Almacena la nacionalidad");

            entity.Property(e => e.PRIORIDAD)
                .HasColumnName("PRIORIDAD");
        }
    }
}
