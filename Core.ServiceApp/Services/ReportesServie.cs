using Core.ServiceApp.DTO.Core_DTOs.Responses;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers.ViewModels;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services
{
    public class ReportesServie : IResportesService
    {
        private readonly ISICM_REPORTES_REPO _reporteRepo;
        private readonly ISICM_ESTATUS_ALERTA_REPO _estatusRepo;

        public ReportesServie (ISICM_REPORTES_REPO reporteRepo, ISICM_ESTATUS_ALERTA_REPO estatusRepo)
        {
            _reporteRepo = reporteRepo;
            _estatusRepo = estatusRepo;
        }

        #region Reporte AK Estatus Casos Por Año
        public async Task<Response> getReporteEstatuspoAño()
        {
            Response response = new Response();
            try
            {
                int años = DateTime.Now.Year - 2012;

                List<EstatusPorAño_Reporte> reporte = new List<EstatusPorAño_Reporte>();
                IEnumerable<SICM_ESTATUS_ALERTA> estatus = await _estatusRepo.GetAll();

                foreach(var item in estatus)
                {
                    EstatusPorAño_Reporte rep = new EstatusPorAño_Reporte();
                    rep.codigoEstatus = item.codigo;
                    rep.Estatus = item.nombre;
                    rep.CantidadPorAnio = new List<int>();
                    rep.anios = new List<int>();
                    reporte.Add(rep);
                }


                for (int i = 0; i <= años; i++)
                {
                    List<EstatusPorAño_VM> res = await _reporteRepo.EstatusPorAño(2012 + i);
                    for(int j = 0; j<reporte.Count; j++)
                    {
                        EstatusPorAño_VM aux = res.Find(x => x.codigo == reporte[j].codigoEstatus);
                        reporte[j].anios.Add(2012 + i);
                        if(aux != null)
                        reporte[j].CantidadPorAnio.Add(aux.cantidad);
                        else
                            reporte[j].CantidadPorAnio.Add(0);

                    }
                }

                response.codigo = 200;
                response.data = reporte;

                return response;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reporte AK Boletines Por Mes
        public async Task<Response> getReporteBoletinesPorMes(int año, int estatus)
        {
            Response res = new Response();
            try
            {
                List<BoletinesPorMes_Reporte> reporte = new List<BoletinesPorMes_Reporte> { 
                    new BoletinesPorMes_Reporte{ codigo = 1, mes = "Enero", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 2, mes = "Febrero", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 3, mes = "Marzo", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 4, mes = "Abril", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 5, mes = "Mayo", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 6, mes = "Junio", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 7, mes = "Julio", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 8, mes = "Agosto", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 9, mes = "Septiembre", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 10, mes = "Octubre", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 11, mes = "Noviembre", hombres = 0, mujeres = 0},
                    new BoletinesPorMes_Reporte{ codigo = 12, mes = "Diciembre", hombres = 0, mujeres = 0}
                };

                List<BoletinesMes_VM> hombres = await _reporteRepo.BoletinesPorMes(año, 1, estatus);

                List<BoletinesMes_VM> mujeres = await _reporteRepo.BoletinesPorMes(año, 2, estatus);

                for(int i = 0; i<reporte.Count; i++)
                {
                    int hcant = hombres.Find(x => x.codigoMes == reporte[i].codigo) != null ? hombres.Find(x => x.codigoMes == reporte[i].codigo).cantidad : 0;
                    int mcant = mujeres.Find(x => x.codigoMes == reporte[i].codigo) != null ? mujeres.Find(x => x.codigoMes == reporte[i].codigo).cantidad : 0;

                    reporte[i].hombres = hcant;
                    reporte[i].mujeres = mcant;
                }

                res.codigo = 200;
                res.data = reporte;
                return res;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
