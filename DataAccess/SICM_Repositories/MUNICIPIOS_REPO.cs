using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.SICM_Repositories
{
    public class MUNICIPIOS_REPO : RepositoryBase<GLOB_CIUDAD>, IMUNICIPIOS_REPO
    {
        // CRUD --> Create, Read, Update, Delete
        // private readonly IDBContext _dbContext;
        private readonly ISICM_DBContext _dbContext;

        public MUNICIPIOS_REPO(ISICM_DBContext dBContext, ISICM_DBContext dbContext) : base(dBContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<GLOB_CIUDAD> getByDivisionesAsync(IEnumerable<GLOB_DIVISION> divs)
        {
            List<int> codigos = divs.Select(x => x.CODIGO_DIVISION).ToList();

            var ciudades = _dbContext.GLOB_CIUDAD.Where(x => codigos.Contains(x.CODIGO_DIVISION)).OrderBy(x => x.DESCRIPCION);

            return ciudades;
        }


    }
}
