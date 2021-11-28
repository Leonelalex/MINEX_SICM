using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class SICM_ESTATUS_ALERTAConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_ESTATUS_ALERTA> entity)
        {
            entity.ToTable("SICM_ESTATUS_BOLETIN");

            entity.HasKey(e => e.codigo).HasName("SICM_ESTATUS_ALERTA_PK");

            entity.Property(e => e.codigo).HasColumnName("CODIGO");

            entity.Property(e => e.nombre).HasColumnName("NOMBRE");

            entity.Property(e => e.descripcion).HasColumnName("DESCRIPCION");

            entity.Property(e => e.activo).HasColumnName("ACTIVO");

            entity.Property(e => e.isAK).HasColumnName("IS_AK");

            entity.Property(e => e.isIC).HasColumnName("IS_IC");
        }
    }
}
