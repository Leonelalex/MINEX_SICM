using DataAccess.Entities.SICM_DbEntities.Generales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class GLOB_TIPO_MISIONConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<GLOB_TIPO_MISION> entity)
        {

            entity.HasKey(e => e.CODIGO_TIPO_MISION)
                   .HasName("PK_MDCG_TIPO_MISION");

            entity.ToTable("GLOB_TIPO_MISION", "glob");

            entity.Property(e => e.CODIGO_TIPO_MISION).HasColumnName("CODIGO_TIPO_MISION");

            entity.Property(e => e.DESCRIPCION)
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.ELIMINADO).HasColumnName("ELIMINADO");

            entity.Property(e => e.FECHA_ELIMINACION)
                .HasColumnName("FECHA_ELIMINACION")
                .HasColumnType("datetime");

            entity.Property(e => e.FECHA_MODIFICACION)
                .HasColumnName("FECHA_MODIFICACION")
                .HasColumnType("datetime");

            entity.Property(e => e.FECHA_REGISTRO)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("datetime");

            entity.Property(e => e.NOMBRE_TIPO_MISION)
                .IsRequired()
                .HasColumnName("NOMBRE_TIPO_MISION")
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.USUARIO_ELIMINA)
                .HasColumnName("USUARIO_ELIMINA")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.USUARIO_MODIFICA)
                .HasColumnName("USUARIO_MODIFICA")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.USUARIO_REGISTRA)
                .HasColumnName("USUARIO_REGISTRA")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
