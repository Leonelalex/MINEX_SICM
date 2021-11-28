using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface ISICM_SITUACION_ALERTA_REPO : IRepositoryBase<SICM_SITUACION_ALERTA>
    {
        Task<IEnumerable<SICM_SITUACION_ALERTA>> getAllActivated();

        string GetNameByIdAsync(int id);
    }
}
