using DataAccess.Entities.SICM_DbEntities.Sistema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig.EntitiesConfigSICM_DB
{
    class Sys_LogConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<SICM_SYSLOG> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("SICM_SYSLOG");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Logged).HasColumnName("Logged");

            entity.Property(e => e.Level).HasColumnName("Level");

            entity.Property(e => e.Message).HasColumnName("Message");
            entity.Property(e => e.UserName).HasColumnName("UserName");
            entity.Property(e => e.Request).HasColumnName("Request");
            entity.Property(e => e.Response).HasColumnName("Response");
            entity.Property(e => e.Url).HasColumnName("Url");
            entity.Property(e => e.Exception).HasColumnName("Exception");
            entity.Property(e => e.ExceptionType).HasColumnName("ExceptionType");
            entity.Property(e => e.InnerException).HasColumnName("InnerException");
            entity.Property(e => e.StackTrace).HasColumnName("StackTrace");
            entity.Property(e => e.IpAdress).HasColumnName("Ip_Adress");

        }
    }
}
