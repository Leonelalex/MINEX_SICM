using DataAccess.Entities.SICM_DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    public static class SICM_PARAMETROS_GENERALESConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_PARAMETROS_GENERALES> entity)
        {
            entity.ToTable("SICM_PARAMETROS_GENERALES");

            entity.HasKey(e => e.codigo);

            entity.Property(e => e.codigo).HasColumnName("CODIGO");

            entity.Property(e => e.nombre).HasColumnName("NOMBRE");

            entity.Property(e => e.valor).HasColumnName("VALOR");
        }
    }
}
