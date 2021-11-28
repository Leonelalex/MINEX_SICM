using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class SICM_ALERTA_MISIONES_REPO : RepositoryBase<SICM_ALERTA_MISIONES>, ISICM_ALERTA_MISIONES_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_ALERTA_MISIONES_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task DeleteByCodigoAlerta(int codAlerta)
        {
            try
            {
                _dbContext.SICM_ALERTA_MISIONES.RemoveRange(
                    _dbContext.SICM_ALERTA_MISIONES.Where(x => x.CodigoAlerta == codAlerta)
                );

                await _dbContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
           
        public async Task<List<string>> getByCodAlerta(int codAlerta)
        {
            try
            {
                List<string> correosMisiones =_dbContext.SICM_ALERTA_MISIONES.Where(x => x.CodigoAlerta == codAlerta)
                    .Join(_dbContext.MISIONES_EXTERIOR, 
                    am => am.CodigoMision,
                    mision => mision.ID_MISION_EXTERIOR, 
                    (am, mision) => mision).Select(x => x.CORREO_ELECTRONICO).ToList();

                return correosMisiones;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
      
    }
}
