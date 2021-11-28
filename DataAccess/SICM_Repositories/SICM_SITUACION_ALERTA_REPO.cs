using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class SICM_SITUACION_ALERTA_REPO : RepositoryBase<SICM_SITUACION_ALERTA>, ISICM_SITUACION_ALERTA_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_SITUACION_ALERTA_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<SICM_SITUACION_ALERTA>> getAllActivated()
        {
            try
            {
                return await _dbContext.SICM_SITUACION_ALERTAS.Where(x => x.Activo == true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GetNameByIdAsync(int id)
        {
            return _dbContext.SICM_SITUACION_ALERTAS.Where(x => x.Codigo == id).First<SICM_SITUACION_ALERTA>().Nombre;
        }
    }
}
