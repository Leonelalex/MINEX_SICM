using DataAccess.Entities.SICM_DbEntities.Catalogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_TEZConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_TEZ> entity)
        {

            entity.HasKey(e => e.Codigo)
                .HasName("PK__SICM_TEZ__CC87E12782733D1B");

            entity.ToTable("SICM_TEZ");

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
