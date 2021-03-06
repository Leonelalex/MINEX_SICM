using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.RepositoryBase;
using External.ServiceApp.Helpers;
using External.ServiceApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static External.ServiceApp.Services.CatalogoService;

namespace DataAccess.SICM_Repositories
{
    public class SICM_TIPO_NARIZ_REPO : RepositoryBase<SICM_TIPO_NARIZ>, ISICM_TIPO_NARIZ_REPO
    {
        // CRUD --> Create, Read, Update, Delete
        private readonly ISICM_DBContext _dbContext;
        public SICM_TIPO_NARIZ_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<bool> ExistAsync(int id)
        {
            try
            {
                if (await _dbContext.SICM_TIPO_NARIZ.CountAsync(x => x.Codigo == id) > 0)
                    return true;
                else
                {
                    SICM_TIPO_NARIZ newcatalogo = new SICM_TIPO_NARIZ();
                    newCatalogo newinfo = new newCatalogo();

                    newinfo = await CatalogoService.mpCatalogos(ConstantsEx.SICM_TIPO_NARIZ, id);

                    if (newinfo.CODIGO != 100 && newinfo.NOMBRE != null)
                    {
                        newcatalogo.Codigo = newinfo.CODIGO;
                        newcatalogo.Nombre = newinfo.NOMBRE;

                        await _dbContext.SICM_TIPO_NARIZ.AddAsync(newcatalogo);
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
