using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.SICM_Repositories
{
    public class SICM_PARAMETROS_GENERALES_REPO : RepositoryBase<SICM_PARAMETROS_GENERALES>, ISICM_PARAMETROS_GENERALES_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_PARAMETROS_GENERALES_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
    }
}
