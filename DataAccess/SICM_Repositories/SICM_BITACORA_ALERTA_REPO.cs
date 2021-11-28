using DataAccess.DBContexts.DBContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.Helpers.ViewModels;
using DataAccess.RepositoryBase;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SICM_Repositories
{
    public class SICM_BITACORA_ALERTA_REPO : RepositoryBase<SICM_BITACORA_ALERTA>, ISICM_BITACORA_ALERTA_REPO
    {
        private readonly ISICM_DBContext _dbContext;

        public SICM_BITACORA_ALERTA_REPO(ISICM_DBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<SICM_BITACORA_ALERTA>> getByCodigoAlerta(int codAlerta)
        {
            try
            {
                IEnumerable<SICM_BITACORA_ALERTA> res = await  _dbContext.SICM_BITACORA_ALERTA.Where(x => x.codigoAlerta == codAlerta)
                    .OrderByDescending(x => x.fecha).ToListAsync();

                return  res;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UltimasAcciones>> getUltimasAcciones(int tipoAlerta)
        {
            try
            {
                List<UltimasAcciones> bitacoras = await _dbContext.SICM_BITACORA_ALERTA
                    .Join(_dbContext.SICM_ALERTAS,
                    bit => bit.codigoAlerta,
                    ale => ale.codigo,
                    (bit, ale) => new UltimasAcciones {
                        codigoAlerta = ale.codigo,
                        numeroAlerta = ale.codigoAlerta,
                        codigoAccion = bit.codigoAccion,
                        codigoEstado = bit.codigoEstado,
                        fechaAccion = bit.fecha,
                        observaciones = bit.observaciones,
                        adjunto = bit.adjunto,
                        usuario = bit.usuario,
                        codigoNotificacion = bit.codigoAccionNotificacion,
                        destinatarios = bit.destinatarios,
                        estadoAlerta = ale.estadoAlerta,
                        numeroOficio = ale.codigoOficio,
                        numeroCaso = ale.numeroCaso,
                        tipoAlerta = ale.tipoAlerta
                    }).Where(x => x.tipoAlerta == tipoAlerta).OrderByDescending(x => x.fechaAccion).ToListAsync();

                return bitacoras;
            }catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
