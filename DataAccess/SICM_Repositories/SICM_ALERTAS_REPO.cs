using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories.RepositoriesContracts
{
    public class SICM_ALERTAS_REPO : RepositoryBase<SICM_ALERTAS>, ISICM_ALERTAS_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_ALERTAS_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<SICM_ALERTAS> getOjAllByCodeAlert(string CodeAlert)
        {
            try
            {
                SICM_ALERTAS res = new SICM_ALERTAS();

                res = await _dbContext.SICM_ALERTAS.Where(x => x.codigoAlerta == CodeAlert).FirstOrDefaultAsync();

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SICM_ALERTAS>> getAlertasAlbaKeneth(PaginationFilter filter)
        {

            return await _dbContext.Set<SICM_ALERTAS>().Where(x => x.tipoAlerta == 1).OrderByDescending(x => x.CreadoFecha)
                .Select(x => new SICM_ALERTAS {
                codigo = x.codigo,
                codigoAlerta = x.codigoAlerta,
                codigoMunicipio = x.codigoMunicipio,
                direccion = x.direccion,
                observaciones = x.observaciones,
                estadoAlerta = x.estadoAlerta,
                fechaActivacion = x.fechaActivacion,
                fechaDesactivacion = x.fechaDesactivacion,
                codigoUsuario = x.codigoUsuario,
                codigoOficio = x.codigoOficio,
                oficio = x.oficio,
                tipoAlerta = x.tipoAlerta,
                correosAdicionales = x.correosAdicionales,
                SicmBoletines = x.SicmBoletines,
                SicmAlertaMisiones = x.SicmAlertaMisiones,
                numeroCaso = x.numeroCaso,
                difusionInternacional = x.difusionInternacional,
                denunciante = x.denunciante
            }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        }
        public async Task<IEnumerable<SICM_ALERTAS>> getAllAlbaKeneth()
        {
            return await _dbContext.SICM_ALERTAS.Where(x => x.tipoAlerta == 1).OrderByDescending(x => x.CreadoFecha)
                .Select(x => new SICM_ALERTAS
            {
                codigo = x.codigo,
                codigoAlerta = x.codigoAlerta,
                codigoMunicipio = x.codigoMunicipio,
                direccion = x.direccion,
                observaciones = x.observaciones,
                estadoAlerta = x.estadoAlerta,
                fechaActivacion = x.fechaActivacion,
                fechaDesactivacion = x.fechaDesactivacion,
                codigoUsuario = x.codigoUsuario,
                codigoOficio = x.codigoOficio,
                oficio = x.oficio,
                tipoAlerta = x.tipoAlerta,
                SicmBoletines = x.SicmBoletines,
                correosAdicionales = x.correosAdicionales,
                SicmAlertaMisiones = x.SicmAlertaMisiones,
                numeroCaso = x.numeroCaso,
                difusionInternacional = x.difusionInternacional,
                denunciante = x.denunciante
                }).ToListAsync();
        }

        public int countAlertasAlbaKeneth()
        {

            return _dbContext.Set<SICM_ALERTAS>().Where(x => x.tipoAlerta == 1).Count();
        }

        public async Task<IEnumerable<SICM_ALERTAS>> getAlertasIsabelClaudina(PaginationFilter filter)
        {
            return await _dbContext.Set<SICM_ALERTAS>().Where(x => x.tipoAlerta == 2).OrderByDescending(x => x.CreadoFecha)
                .Select(x => new SICM_ALERTAS
            {
                codigo = x.codigo,
                codigoAlerta = x.codigoAlerta,
                codigoMunicipio = x.codigoMunicipio,
                direccion = x.direccion,
                observaciones = x.observaciones,
                estadoAlerta = x.estadoAlerta,
                fechaActivacion = x.fechaActivacion,
                fechaDesactivacion = x.fechaDesactivacion,
                codigoUsuario = x.codigoUsuario,
                codigoOficio = x.codigoOficio,
                oficio = x.oficio,
                tipoAlerta = x.tipoAlerta,
                correosAdicionales = x.correosAdicionales,
                SicmBoletines = x.SicmBoletines,
                SicmAlertaMisiones = x.SicmAlertaMisiones
            }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<SICM_ALERTAS>> getAllIsabelClaudina()
        {
            return await _dbContext.SICM_ALERTAS.Where(x => x.tipoAlerta == 2).OrderByDescending(x => x.CreadoFecha)
                .Select(x => new SICM_ALERTAS
            {
                codigo = x.codigo,
                codigoAlerta = x.codigoAlerta,
                codigoMunicipio = x.codigoMunicipio,
                direccion = x.direccion,
                observaciones = x.observaciones,
                estadoAlerta = x.estadoAlerta,
                fechaActivacion = x.fechaActivacion,
                fechaDesactivacion = x.fechaDesactivacion,
                codigoUsuario = x.codigoUsuario,
                codigoOficio = x.codigoOficio,
                oficio = x.oficio,
                tipoAlerta = x.tipoAlerta,
                correosAdicionales = x.correosAdicionales,
                SicmBoletines = x.SicmBoletines,
                SicmAlertaMisiones = x.SicmAlertaMisiones,
                difusionInternacional= x.difusionInternacional
            }).ToListAsync();
        }

        public int countAlertasIsabelClaudina()
        {
            return _dbContext.Set<SICM_ALERTAS>().Where(x => x.tipoAlerta == Constants.IsabelClaudina).Count();
        }

        public async Task<SICM_ALERTAS> getAlertaByCodigo(string codigoAlerta)
        {
            try
            {
                SICM_ALERTAS alerta = await _dbContext.SICM_ALERTAS.Where(y => y.codigoAlerta == codigoAlerta).Select(x => new SICM_ALERTAS
                {
                    codigo = x.codigo,
                    codigoAlerta = x.codigoAlerta,
                    codigoMunicipio = x.codigoMunicipio,
                    direccion = x.direccion,
                    observaciones = x.observaciones,
                    estadoAlerta = x.estadoAlerta,
                    fechaActivacion = x.fechaActivacion,
                    fechaDesactivacion = x.fechaDesactivacion,
                    codigoUsuario = x.codigoUsuario,
                    codigoOficio = x.codigoOficio,
                    oficio = x.oficio,
                    tipoAlerta = x.tipoAlerta,
                    //codigoSituacion = x.codigoSituacion,
                    correosAdicionales = x.correosAdicionales,
                    SicmBoletines = x.SicmBoletines,
                    SicmAlertaMisiones = x.SicmAlertaMisiones,
                    difusionInternacional = x.difusionInternacional
                }).FirstOrDefaultAsync();

                //if (alerta.codigo == 0)
                //    alerta.codigo = 0;

                return alerta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SICM_ALERTAS>> getAlertaICSinActivar()
        {
 
            IEnumerable<SICM_ALERTAS> alertas = await _dbContext.SICM_ALERTAS.Where(y => y.tipoAlerta == 2 &&
            y.estadoAlerta == 1).OrderByDescending(x => x.CreadoFecha).Select(x => new SICM_ALERTAS
            {
                codigo = x.codigo,
                codigoAlerta = x.codigoAlerta,
                codigoMunicipio = x.codigoMunicipio,
                direccion = x.direccion,
                observaciones = x.observaciones,
                estadoAlerta = x.estadoAlerta,
                fechaActivacion = x.fechaActivacion,
                fechaDesactivacion = x.fechaDesactivacion,
                codigoUsuario = x.codigoUsuario,
                codigoOficio = x.codigoOficio,
                oficio = x.oficio,
                tipoAlerta = x.tipoAlerta,
                correosAdicionales = x.correosAdicionales,
                SicmBoletines = x.SicmBoletines,
                SicmAlertaMisiones = x.SicmAlertaMisiones,
                difusionInternacional = x.difusionInternacional
            }).ToListAsync();

            return alertas;
        }
    }
}
