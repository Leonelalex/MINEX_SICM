using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class GLOB_PAIS_REPO : RepositoryBase<GLOB_PAIS>, IGLOB_PAIS_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public GLOB_PAIS_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<GLOB_PAIS>> getPaisesConMision()
        {
            List<int> codmisiones = await _dbContext.MISIONES_EXTERIOR.Select(x => x.CODIGO_PAIS).Distinct().ToListAsync();
            IEnumerable<GLOB_PAIS> paises = _dbContext.GLOB_PAIS.Where(x => codmisiones.Contains(x.CODIGO_PAIS)).OrderBy(x => x.PRIORIDAD).ThenBy(x => x.DESCRIPCION);

            paises.ElementAt(3).DESCRIPCION = "Embajadas acreditadas en Guatemala";

            return paises;
        }
    }
}
