using DataAccess.DBContexts.DBContracts;
using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.RepositoryBase
{
    public class RepositoryBase<Entidad> : IRepositoryBase<Entidad> where Entidad : class
    {

        // CRUD --> Create, Read, Update, Delete
        private readonly ISICM_DBContext _dbContext;

        public RepositoryBase(ISICM_DBContext dBContext)
        {
            _dbContext = dBContext;
        }
  
        public async Task<Entidad> Add(Entidad entidad)
        {
            try
            {
                await _dbContext.Set<Entidad>().AddAsync(entidad);

                await _dbContext.SaveChangesAsync();

                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await GetByID(id);

                _dbContext.Set<Entidad>().Remove(entity);

                await _dbContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var entity = await GetByID(id);

                _dbContext.Set<Entidad>().Remove(entity);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Exist(System.Linq.Expressions.Expression<Func<Entidad, bool>> expression)
        {
            try
            {
                return await _dbContext.Set<Entidad>().AnyAsync(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Entidad>> Find(System.Linq.Expressions.Expression<Func<Entidad, bool>> expression)
        {
            try
            {
                return await _dbContext.Set<Entidad>().Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Recibe el where como expresion
        public async Task<Entidad> FindFirstOrDefault(System.Linq.Expressions.Expression<Func<Entidad, bool>> expression)
        {
            try
            {
                return await _dbContext.Set<Entidad>().FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Recibe el where como expresion
        public async Task<Entidad> FindSingleOrDefault(System.Linq.Expressions.Expression<Func<Entidad, bool>> expression)
        {
            try
            {
                return await _dbContext.Set<Entidad>().SingleOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //utilizaremos este metodo 
        public async Task<IEnumerable<Entidad>> GetAll()
        {
            try
            {
                return await _dbContext.Set<Entidad>().ToListAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Entidad>> GetAllDesc()
        {
            try
            {
                IEnumerable<Entidad>  result  = await _dbContext.Set<Entidad>().ToListAsync();
                result = result.Reverse();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Entidad>> GetAllPaginated(PaginationFilter filter)
        {
            try
            {
                return await _dbContext.Set<Entidad>().Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Entidad> GetAllEntity()
        {
            try
            {
                return _dbContext.Set<Entidad>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<Entidad> GetByID(int id)
        {
            try
            {
                var entidad = await _dbContext
                   .Set<Entidad>()
                   .FindAsync(id);

                if (entidad == null)
                    return null;

                _dbContext.Entry<Entidad>(entidad).State = EntityState.Detached;

                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entidad> GetByID(string id)
        {
            try
            {
                var entidad = await _dbContext
                    .Set<Entidad>()
                    .FindAsync(id);

                _dbContext.Entry<Entidad>(entidad).State = EntityState.Detached;

                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entidad> Update(Entidad entidad)
        {
            try
            {
                _dbContext.Set<Entidad>().Update(entidad);

                await _dbContext.SaveChangesAsync();

                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
