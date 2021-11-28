using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class DEPARTAMENTOS_REPO : RepositoryBase<GLOB_DIVISION>, IDEPARTAMENTOS_REPO
    {
        // CRUD --> Create, Read, Update, Delete
        // private readonly IDBContext _dbContext;
        private readonly ISICM_DBContext _dbContext;

        public DEPARTAMENTOS_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<GLOB_DIVISION>> getByPais(int codigoPais)
        {
            var divisiones = _dbContext.GLOB_DIVISION.Where(x => x.CODIGO_PAIS == codigoPais).OrderBy(x => x.DESCRIPCION);
            return divisiones;
        }
    }
}
