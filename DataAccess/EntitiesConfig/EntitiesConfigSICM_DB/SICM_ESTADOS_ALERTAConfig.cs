using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_ESTADOS_ALERTAConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_ESTADOS_ALERTA> entity)
        {
            entity.HasKey(e => e.CodigoEstado);

            entity.ToTable("SICM_ESTADOS_ALERTA");

            entity.Property(e => e.CodigoEstado)
                .HasColumnName("CODIGO_ESTADO")
                .ValueGeneratedNever();

            entity.Property(e => e.Descripcion)
                .HasColumnName("DESCRIPCION")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("NOMBRE")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
