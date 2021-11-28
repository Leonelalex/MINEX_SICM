using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.DBContexts.DBContracts
{
    public interface ISICM_DBContext
    {
        DbSet<SICM_TEZ> SICM_TEZ { get; set; }
        DbSet<SICM_COLOR_CABELLO> SICM_COLOR_CABELLO { get; set; }
        DbSet<SICM_COLOR_OJOS> SICM_COLOR_OJOS { get; set; }
        DbSet<SICM_COMPLEXION> SICM_COMPLEXION { get; set; }
        DbSet<SICM_TAMANIO_CABELLO> SICM_TAMANIO_CABELLO { get; set; }
        DbSet<SICM_TIPO_CABELLO> SICM_TIPO_CABELLO { get; set; }
        DbSet<SICM_TIPO_CEJA> SICM_TIPO_CEJA { get; set; }
        DbSet<SICM_TIPO_NARIZ> SICM_TIPO_NARIZ { get; set; }
        DbSet<SICM_ALERTAS> SICM_ALERTAS { get; set; }
        DbSet<SICM_BOLETINES> SICM_BOLETINES { get; set; }

        DbSet<GLOB_DIVISION> GLOB_DIVISION { get; set; }
        DbSet<GLOB_CIUDAD> GLOB_CIUDAD { get; set; }
        DbSet<GLOB_PAIS> GLOB_PAIS { get; set; }
        DbSet<GLOB_MISIONES_EXTERIOR> MISIONES_EXTERIOR { get; set; }
        DbSet<GLOB_TIPO_MISION> TIPO_MISION { get; set; }

        DbSet<GLOB_GENERO> GLOB_GENERO { get; set; }
        DbSet<SICM_ESTADOS_ALERTA> SICM_ESTADOS_ALERTA { get; set; }
        DbSet<SICM_BITACORA_ALERTA> SICM_BITACORA_ALERTA { get; set; }
        DbSet<SICM_ALERTA_MISIONES> SICM_ALERTA_MISIONES { get; set; }
        DbSet<SICM_SITUACION_ALERTA> SICM_SITUACION_ALERTAS { get; set; }
        DbSet<SICM_ESTATUS_ALERTA> SICM_ESTATUS_ALERTA { get; set; }
        DbSet<SICM_SYSLOG> SICM_SYSLOG { get; set; }
        DbSet<SICM_ACCIONES_NOTIFICACION> SICM_ACCIONES_NOTIFICACION { get; set; }
        DbSet<SICM_PARAMETROS_GENERALES> SICM_PARAMETROS_GENERALES { get; set; }


        #region Propiedades
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        #endregion

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        //Task<int> AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default(CancellationToken));

        void AddRange(IEnumerable<object> entities);
        void RemoveRange(IEnumerable<object> entities);
        EntityEntry Update(object entity);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
    }
}
