
using DataAccess.Entities.AlertEntities;
using DataAccess.EntityConfig.EntityConfigAlert;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.DBContexts
{
    public partial class AlertDB_Context : DbContext//, IAlertDBContext
    {
        //constructor vacio para utilizar en servicios y controladores
        public AlertDB_Context()
        {
        }

        public AlertDB_Context(DbContextOptions<AlertDB_Context> options) : base(options)
        {

        }
       
        public virtual DbSet<SICM_TEZ> SICM_TEZ { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlServer();
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");


            // -----------------------------------------------------------------------------------------------------
            SICM_TEZConfig.SetEntityBuilder(modelBuilder.Entity<SICM_TEZ>());
        }

    }
}
