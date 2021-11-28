using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.Helpers;
using DataAccess.RepositoryBase;
using External.ServiceApp.Helpers;
using External.ServiceApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using static External.ServiceApp.Services.CatalogoService;

namespace DataAccess.SICM_Repositories
{
    public class SICM_COLOR_CABELLO_REPO : RepositoryBase<SICM_COLOR_CABELLO>, ISICM_COLOR_CABELLO_REPO
    {       
        private readonly ISICM_DBContext _dbContext;

        public SICM_COLOR_CABELLO_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<bool> ExistAsync(int id)
        {
            try
            {
                if (await _dbContext.SICM_COLOR_CABELLO.CountAsync(x => x.Codigo == id) > 0)
                    return true;
                else
                {
                    SICM_COLOR_CABELLO newcatalogo = new SICM_COLOR_CABELLO();
                    newCatalogo newinfo = new newCatalogo();

                    newinfo = await CatalogoService.mpCatalogos(ConstantsEx.SICM_COLOR_CABELLO, id);

                    if (newinfo.CODIGO != 100 || newinfo.NOMBRE != null)
                    {
                        newcatalogo.Codigo = newinfo.CODIGO;
                        newcatalogo.Nombre = newinfo.NOMBRE;

                        await _dbContext.SICM_COLOR_CABELLO.AddAsync(newcatalogo);

                        await _dbContext.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
