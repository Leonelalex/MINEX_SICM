using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_ACCIONES_NOTIFICACIONConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_ACCIONES_NOTIFICACION> entity)
        {
            entity.ToTable("SICM_ACCIONES_NOTIFICACION");

            entity.HasKey(e => e.codigo);

            entity.Property(e => e.nombre).HasColumnName("NOMBRE");

            entity.Property(e => e.descripcion).HasColumnName("DESCRIPCION");

            entity.Property(e => e.activo).HasColumnName("ACTIVO");

            entity.Property(e => e.isAK).HasColumnName("IS_AK");

            entity.Property(e => e.isIC).HasColumnName("IS_IC");
        }
    }
}
