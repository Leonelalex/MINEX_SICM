using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public interface IMUNICIPIOS_REPO : IRepositoryBase<GLOB_CIUDAD>
    {
        IEnumerable<GLOB_CIUDAD> getByDivisionesAsync(IEnumerable<GLOB_DIVISION> divs);
    }


}
