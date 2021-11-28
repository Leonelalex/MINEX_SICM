using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.EntitiesConfig.EntitiesConfigSICM_DB;
using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.DBContexts
{
    public partial class SICM_DBContext : DbContext, ISICM_DBContext
    {
        //constructor vacio para utilizar en servicios y controladores
        public SICM_DBContext()
        {
        }

        public SICM_DBContext(DbContextOptions<SICM_DBContext> options) : base(options)
        {
        }

        public virtual DbSet<SICM_TEZ> SICM_TEZ { get; set; }
        public virtual DbSet<SICM_COLOR_CABELLO> SICM_COLOR_CABELLO { get; set; }
        public virtual DbSet<SICM_COLOR_OJOS> SICM_COLOR_OJOS { get; set; }
        public virtual DbSet<SICM_COMPLEXION> SICM_COMPLEXION { get; set; }
        public virtual DbSet<SICM_TAMANIO_CABELLO> SICM_TAMANIO_CABELLO { get; set; }
        public virtual DbSet<SICM_TIPO_CABELLO> SICM_TIPO_CABELLO { get; set; }
        public virtual DbSet<SICM_TIPO_CEJA> SICM_TIPO_CEJA { get; set; }
        public virtual DbSet<SICM_TIPO_NARIZ> SICM_TIPO_NARIZ { get; set; }
        public virtual DbSet<GLOB_DIVISION> GLOB_DIVISION { get; set; }
        public virtual DbSet<GLOB_CIUDAD> GLOB_CIUDAD { get; set; }
        public virtual DbSet<GLOB_PAIS> GLOB_PAIS { get; set; }
        public virtual DbSet<GLOB_GENERO> GLOB_GENERO { get; set; }
        public virtual DbSet<SICM_ALERTAS> SICM_ALERTAS { get; set; }
        public virtual DbSet<SICM_ESTADOS_ALERTA> SICM_ESTADOS_ALERTA { get; set; }
        public virtual DbSet<SICM_ACCIONES_ALERTA> SICM_ACCIONES_ALERTA { get; set; }
        public virtual DbSet<SICM_BITACORA_ALERTA> SICM_BITACORA_ALERTA { get; set; }
        public virtual DbSet<SICM_AVISTAMIENTOS> SICM_AVISTAMIENTOS { get; set; }
        public virtual DbSet<SICM_ALERTA_MISIONES> SICM_ALERTA_MISIONES { get; set; }
        public virtual DbSet<SICM_SITUACION_ALERTA> SICM_SITUACION_ALERTAS { get; set; }
        public virtual DbSet<GLOB_MISIONES_EXTERIOR> MISIONES_EXTERIOR { get; set; }
        public virtual DbSet<GLOB_TIPO_MISION> TIPO_MISION { get; set; }
        public virtual DbSet<SICM_BOLETINES> SICM_BOLETINES { get; set; }
        public virtual DbSet<SICM_ESTATUS_ALERTA> SICM_ESTATUS_ALERTA { get; set; }
        public virtual DbSet<SICM_SYSLOG> SICM_SYSLOG { get; set; }
        public virtual DbSet<SICM_ACCIONES_NOTIFICACION> SICM_ACCIONES_NOTIFICACION { get; set; }
        public virtual DbSet<SICM_PARAMETROS_GENERALES> SICM_PARAMETROS_GENERALES { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseSqlServer();
                //Startup.Configuration.Get("Data:DefaultConnection:ConnectionString")
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            // -----------------------------------------------------------------------------------------------------
            SICM_COLOR_CABELLOConfig.SetEntityBuilder(modelBuilder.Entity<SICM_COLOR_CABELLO>());
            SICM_COLOR_OJOSConfig.SetEntityBuilder(modelBuilder.Entity<SICM_COLOR_OJOS>());
            SICM_COMPLEXIONConfig.SetEntityBuilder(modelBuilder.Entity<SICM_COMPLEXION>());
            SICM_TAMANIO_CABELLOConfig.SetEntityBuilder(modelBuilder.Entity<SICM_TAMANIO_CABELLO>());
            SICM_TEZConfig.SetEntityBuilder(modelBuilder.Entity<SICM_TEZ>());
            SICM_TIPO_ALERTAConfig.SetEntityBuilder(modelBuilder.Entity<SICM_TIPO_ALERTA>());
            SICM_TIPO_CABELLOConfig.SetEntityBuilder(modelBuilder.Entity<SICM_TIPO_CABELLO>());
            SICM_TIPO_CEJAConfig.SetEntityBuilder(modelBuilder.Entity<SICM_TIPO_CEJA>());
            SICM_TIPO_NARIZConfig.SetEntityBuilder(modelBuilder.Entity<SICM_TIPO_NARIZ>());
            SICM_ALERTASConfig.SetEntityBuilder(modelBuilder.Entity<SICM_ALERTAS>());
            SICM_ESTADOS_ALERTAConfig.SetEntityBuilder(modelBuilder.Entity<SICM_ESTADOS_ALERTA>());
            SICM_BITACORA_ALERTAConfig.SetEntityBuilder(modelBuilder.Entity<SICM_BITACORA_ALERTA>());
            SICM_ACCIONES_ALERTAConfig.SetEntityBuilder(modelBuilder.Entity<SICM_ACCIONES_ALERTA>());
            SICM_AVISTAMIENTOSConfig.SetEntityBuilder(modelBuilder.Entity<SICM_AVISTAMIENTOS>());
            SICM_ALERTA_MISIONESConfig.SetEntityBuilder(modelBuilder.Entity<SICM_ALERTA_MISIONES>());
            SICM_SITUACION_ALERTAConfig.SetEntityBuilder(modelBuilder.Entity<SICM_SITUACION_ALERTA>());
            SICM_BOLETINESConfig.SetEntityBuilder(modelBuilder.Entity<SICM_BOLETINES>());
            SICM_ESTATUS_ALERTAConfig.SetEntityBuilder(modelBuilder.Entity<SICM_ESTATUS_ALERTA>());
            SICM_ACCIONES_NOTIFICACIONConfig.SetEntityBuilder(modelBuilder.Entity<SICM_ACCIONES_NOTIFICACION>());
            SICM_PARAMETROS_GENERALESConfig.SetEntityBuilder(modelBuilder.Entity<SICM_PARAMETROS_GENERALES>());

            GLOB_DIVISIONConfig.SetEntityBuilder(modelBuilder.Entity<GLOB_DIVISION>());
            GLOB_CIUDADConfig.SetEntityBuilder(modelBuilder.Entity<GLOB_CIUDAD>());
            GLOB_PAISConfig.SetEntityBuilder(modelBuilder.Entity<GLOB_PAIS>());
            GLOB_GENEROConfig.SetEntityBuilder(modelBuilder.Entity<GLOB_GENERO>());
            GLOB_MISIONES_EXTERIORConfig.SetEntityBuilder(modelBuilder.Entity<GLOB_MISIONES_EXTERIOR>());
            GLOB_TIPO_MISIONConfig.SetEntityBuilder(modelBuilder.Entity<GLOB_TIPO_MISION>());

            Sys_LogConfig.SetEntityBuilder(modelBuilder.Entity<SICM_SYSLOG>());


            // Codigo agregado de más
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ProcessSave();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void ProcessSave()
        {
            var currentTime = DateTime.Now;

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is AuditEntityBase))
            {

                var entity = item.Entity as AuditEntityBase;

                entity.CreadoPor = ""; //currentUserService.GetCurrentUserId();
                entity.CreadoFecha = currentTime;
                entity.ActualizadoPor = ""; //currentUserService.GetCurrentUserId();
                entity.ActualizadoFecha = currentTime;
                entity.EliminadoPor = ""; //currentUserService.GetCurrentUserId();
                entity.EliminadoFecha = currentTime;
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is AuditEntityBase))
            {
                var entity = item.Entity as AuditEntityBase;
                entity.ActualizadoPor = ""; //currentUserService.GetCurrentUserId();
                entity.ActualizadoFecha = currentTime;

                item.Property(nameof(entity.CreadoPor)).IsModified = false;
                item.Property(nameof(entity.CreadoFecha)).IsModified = false;
                item.Property(nameof(entity.EliminadoPor)).IsModified = false;
                item.Property(nameof(entity.EliminadoFecha)).IsModified = false;
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is AuditEntityBase))
            {
                var entity = item.Entity as AuditEntityBase;
                entity.EliminadoPor = ""; //currentUserService.GetCurrentUserId();
                entity.EliminadoFecha = currentTime;

                item.Property(nameof(entity.CreadoPor)).IsModified = false;
                item.Property(nameof(entity.CreadoFecha)).IsModified = false;
                item.Property(nameof(entity.ActualizadoPor)).IsModified = false;
                item.Property(nameof(entity.ActualizadoFecha)).IsModified = false;
            }

            // throw new NotImplementedException();
        }
    }
}
