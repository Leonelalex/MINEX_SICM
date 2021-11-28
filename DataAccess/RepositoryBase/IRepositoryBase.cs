using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.RepositoryBase
{
    public interface IRepositoryBase<Entidad> where Entidad : class
    {
        Task<IEnumerable<Entidad>> GetAll();
        Task<IEnumerable<Entidad>> GetAllPaginated(PaginationFilter filter);

        //devolver todas pero no asincrono
        IEnumerable<Entidad> GetAllEntity();
        Task<Entidad> GetByID(int id);
        Task<Entidad> GetByID(string id);
        Task<IEnumerable<Entidad>> Find(Expression<Func<Entidad, bool>> expression);
        Task<Entidad> FindSingleOrDefault(Expression<Func<Entidad, bool>> expression);
        Task<Entidad> FindFirstOrDefault(Expression<Func<Entidad, bool>> expression);
        Task<Entidad> Add(Entidad entidad);
        //Task<Entidad> Actualizar(int id, Entidad entidad);
        Task<Entidad> Update(Entidad entidad);
        Task Delete(int id);
        Task Delete(string id);
        Task<bool> Exist(Expression<Func<Entidad, bool>> expression);
        //  Task<bool> ExistAsync(int id);

        Task<IEnumerable<Entidad>> GetAllDesc();
    }
}
