using DataAccess.Entities.SICM_DbEntities.Catalogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_TAMANIO_CABELLOConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_TAMANIO_CABELLO> entity)
        {
            entity.HasKey(e => e.Codigo)
                .HasName("PK__SICM_TAM__CC87E127D83D80E1");

            entity.ToTable("SICM_TAMANIO_CABELLO");

            entity.Property(e => e.Codigo)
                .HasColumnName("CODIGO")
                .ValueGeneratedNever();

            entity.Property(e => e.Nombre)
                .HasColumnName("NOMBRE")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
