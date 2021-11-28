using DataAccess.Entities.SICM_DbEntities.Generales;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Helpers.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using DataAccess.DBContexts.DBContracts;

namespace DataAccess.SICM_Repositories
{
    public class GLOB_MISIONES_EXTERIOR_REPO : RepositoryBase<GLOB_MISIONES_EXTERIOR>, IGLOB_MISIONES_EXTERIOR_REPO
    {
        private readonly ISICM_DBContext _dbContext;
        public GLOB_MISIONES_EXTERIOR_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<List<misionesViewModel>> getMisiones()
        {
            List<misionesViewModel> misiones = await _dbContext.MISIONES_EXTERIOR
                .Join(_dbContext.TIPO_MISION,
                mision => mision.CODIGO_TIPO_MISION,
                tipo => tipo.CODIGO_TIPO_MISION,
                (mision, tipo) => new misionesViewModel
                {
                    ID_MISION_EXTERIOR = mision.ID_MISION_EXTERIOR,
                    NOMBRE_MISION = mision.NOMBRE_MISION,
                    NOMBRE_TIPO_MISION = tipo.NOMBRE_TIPO_MISION,
                    CODIGO_PAIS = mision.CODIGO_PAIS,
                    PRIORIDAD = mision.PRIORIDAD
                }).OrderBy(x => x.PRIORIDAD).ToListAsync();

            return misiones;
        }

        public async Task<List<emailsMisiones>> GetEmails(List<int> IdMisions)
        {
            try
            {
                List<emailsMisiones> emailsMisiones = await _dbContext.MISIONES_EXTERIOR.Where(x => IdMisions.Contains(x.ID_MISION_EXTERIOR)).Select(x => 
                new emailsMisiones
                {
                    To = x.CORREO_ELECTRONICO
                }).ToListAsync();

                return emailsMisiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
