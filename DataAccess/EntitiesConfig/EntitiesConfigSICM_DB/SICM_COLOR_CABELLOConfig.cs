using DataAccess.Entities.SICM_DbEntities.Catalogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public class SICM_COLOR_CABELLOConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_COLOR_CABELLO> entity)
        {

            entity.HasKey(e => e.Codigo)
                   .HasName("PK__SICM_COL__CC87E127ACE42C8A");

            entity.ToTable("SICM_COLOR_CABELLO");

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
