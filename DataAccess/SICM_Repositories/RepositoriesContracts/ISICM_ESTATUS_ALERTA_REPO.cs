using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public interface ISICM_ESTATUS_ALERTA_REPO : IRepositoryBase<SICM_ESTATUS_ALERTA>
    {
        string GetNameByIdAsync(int id);
    }
}
