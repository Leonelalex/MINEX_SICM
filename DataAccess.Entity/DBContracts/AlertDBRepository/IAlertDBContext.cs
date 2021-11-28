using DataAccess.Entities.AlertEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.DBContracts.AlertDBRepository
{
    //Creamos la inteface para el repositorio pero solo cuando la entidad que recibe sea de tipo class
    public interface IAlertDBContext
    {

        // -----------------------------------------------------------------------------------------------------
        DbSet<SICM_TEZ> SICM_TEZ { get; set; }
        // -----------------------------------------------------------------------------------------------------

        #region Propiedades
        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }
        #endregion

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        //Task<int> AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default(CancellationToken));
        //void AddRange(IEnumerable<object> entities);

        void RemoveRange(IEnumerable<object> entities);

        EntityEntry Update(object entity);

        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        EntityEntry Entry(object entity);


    }
}
}
