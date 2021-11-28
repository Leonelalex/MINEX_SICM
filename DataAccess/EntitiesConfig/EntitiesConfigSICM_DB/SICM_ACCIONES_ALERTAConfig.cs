using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_ACCIONES_ALERTAConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_ACCIONES_ALERTA> entity)
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("SICM_ACCIONES_ALERTA");

            entity.Property(e => e.Nombre).HasColumnName("NOMBRE");

            entity.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");

            entity.Property(e => e.ContenidoEmail).HasColumnName("CONTENIDO_EMAIL");

        }
    }
}
