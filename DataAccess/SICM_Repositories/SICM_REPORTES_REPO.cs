using DataAccess.DBContexts.DBContracts;
using DataAccess.Helpers.ViewModels;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class SICM_REPORTES_REPO : ISICM_REPORTES_REPO
    {
        private readonly ISICM_DBContext _dbContext;
        public SICM_REPORTES_REPO(ISICM_DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<List<EstatusPorAño_VM>> EstatusPorAño(int año)
        {
            try
            {
                var result =  _dbContext.SICM_BOLETINES
                    .Join(_dbContext.SICM_ALERTAS,
                    boletines => boletines.codigoAlerta,
                    alertas => alertas.codigo,
                    (boletines, alertas) => new { boletines, alertas })
                    .Join(_dbContext.SICM_ESTATUS_ALERTA,
                    ab => ab.boletines.codigoEstatus,
                    estatus => estatus.codigo,
                    (ab, estatus) => new { ab, estatus })
                    .Where(x => x.ab.alertas.fechaActivacion.Year == año)
                    .GroupBy(x => x.estatus.codigo)
                    .Select(grupo => new EstatusPorAño_VM
                    {
                        codigo = grupo.Key,
                        cantidad = grupo.Count()
                    }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<BoletinesMes_VM>> BoletinesPorMes(int año, int genero, int estado)
        {
            List<BoletinesMes_VM> result;
            try
            {
                result = _dbContext.SICM_BOLETINES
                    .Join(_dbContext.SICM_ALERTAS,
                    boletines => boletines.codigoAlerta,
                    alertas => alertas.codigo,
                    (boletines, alertas) => new { boletines, alertas })
                    .Where(x => x.alertas.estadoAlerta == estado && x.alertas.fechaActivacion.Year == año && x.boletines.genero == genero)
                    .GroupBy(x => x.alertas.fechaActivacion.Month)
                    .Select(grupo => new BoletinesMes_VM
                    {
                        codigoMes = grupo.Key,
                        cantidad = grupo.Count()
                    }).ToList();

                return result;
            }
            catch
            {
                result = new List<BoletinesMes_VM>();
                BoletinesMes_VM def = new BoletinesMes_VM { codigoMes = 0, cantidad = 0 };
                result.Add(def);
                return result;
            }
        }
       
    }
}
