using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.DBContracts.AlertDBRepository.Repositories
{
    //asignamos los metodos genericos que tengamos en el repositorio base
    public interface IAlertDBRepositoryBase<Entidad> where Entidad : class
    {

        Task<Entidad> GetAsync(int filter);

        // Task<Entidad> Obtener(string id);

        Task<Entidad> AddAsync(Entidad entidad);

        //Task<Entidad> Get(int id, Entidad entidad);

        Task<Entidad> EditAsync(Entidad entidad);

        Task RemoveAsync(int id);

        Task RemoveAsync(string id);

        Task<IEnumerable<Entidad>> GetAllAsync();

        Task<IEnumerable<Entidad>> FindAsync(Expression<Func<Entidad, bool>> expression);

        Task<Entidad> FindSingleOrDefaultAsync(Expression<Func<Entidad, bool>> expression);

        Task<Entidad> FindFirstOrDefaultAsync(Expression<Func<Entidad, bool>> expression);

        Task<bool> ExistAsync(Expression<Func<Entidad, bool>> expression);

    }
}
