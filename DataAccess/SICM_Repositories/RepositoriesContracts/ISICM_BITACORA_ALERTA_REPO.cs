using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.Helpers.ViewModels;
using DataAccess.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface ISICM_BITACORA_ALERTA_REPO : IRepositoryBase<SICM_BITACORA_ALERTA>
    {
        Task<IEnumerable<SICM_BITACORA_ALERTA>> getByCodigoAlerta(int codAlerta);
        Task<List<UltimasAcciones>> getUltimasAcciones(int tipoAlerta);
    }
}
