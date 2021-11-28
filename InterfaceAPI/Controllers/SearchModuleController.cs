using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.ViewModels;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using External.ServiceApp.Helpers;
using External.ServiceApp.Services;
using External.ServiceApp.DTOs;
using Core.ServiceApp.Utils.UtilContracts;

namespace InterfaceApi.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchModuleController : ControllerBase
    {
        private readonly ISearchModuleService _serachModuleService;
        private readonly ILogUtils _logservice;

        public SearchModuleController(ISearchModuleService service, ILogUtils logservice)
        {
            _serachModuleService = service;
            _logservice = logservice;
        }
        #region Busqueda de Personas 
        #region comments, swagger info
        /// <summary>
        /// Busqueda de alertas locales por nommbre y cui de la persona
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>Un listado con las alertas que coincidan con los datos de busqueda recividos</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("persona")]
        public async Task<ActionResult> getPersona(persona_ViewModel persona, [FromQuery] PaginationFilter pagefilter, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - search/persona";
            log.Request = JsonSerializer.Serialize(persona);
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.SEARCH_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _serachModuleService.buscarPersonaLocal(persona, pagefilter);
                    if (servRes.codigo == Constants.OK_code)
                    {
                        log.Level = Constants.level_INFO;
                        Response.Headers.Add(Constants.SessionToken, ssoRes.newToken);
                        return Ok(servRes.data);
                    }
                    else
                    {
                        log.Level = Constants.level_ERROR;
                        var serverError = new ErrorResponse { codigo = servRes.codigo, message = servRes.message };
                        log.Response = JsonSerializer.Serialize(serverError);
                        log.Message = servRes.message;
                        log.InnerException = servRes.innerError;
                        return StatusCode(servRes.codigo, serverError);
                    }
                }

                log.Level = Constants.level_WARN;
                var err = new ErrorResponse { codigo = ssoRes.codigo, message = Constants.NoAutorized_msj };
                log.Response = JsonSerializer.Serialize(err);
                return StatusCode(ssoRes.codigo, err);

            }
            catch(Exception ex)
            {
                log.Level = Constants.level_ERROR;
                var Error = new ErrorResponse { codigo = Constants.Error_code, message = ex.Message };
                log.Response = JsonSerializer.Serialize(Error);
                log.Message = ex.Message;
                log.InnerException = ex.InnerException.Message;
                return StatusCode(Constants.Error_code, Error);
            }
            finally
            {
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Busqueda de Personas por codigo de Alerta
        #region comments, swagger info
        /// <summary>
        /// Busqueda de persona por codigo de alerta
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAlerta">Codigo de alerta a buscar</param>
        /// <returns>Retorna el registro que coincida con el codigo de la alerta recivido</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("alerta/{codAlerta}")]
        public async Task<ActionResult> getalerta(string codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - search/alerta/" + codAlerta;
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.SEARCH_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _serachModuleService.buscarAlertaporCodigo(codAlerta);
                    if (servRes.codigo == 200)
                    {
                        log.Level = Constants.level_INFO;
                        Response.Headers.Add(Constants.SessionToken, ssoRes.newToken);
                        return Ok(servRes.data);
                    }
                    else
                    {
                        log.Level = Constants.level_ERROR;
                        var serverError = new ErrorResponse { codigo = servRes.codigo, message = servRes.message };
                        log.Response = JsonSerializer.Serialize(serverError);
                        log.Message = servRes.message;
                        log.InnerException = servRes.innerError;
                        return StatusCode(servRes.codigo, serverError);
                    }
                }

                log.Level = Constants.level_WARN;
                var err = new ErrorResponse { codigo = ssoRes.codigo, message = Constants.NoAutorized_msj };
                log.Response = JsonSerializer.Serialize(err);
                return StatusCode(ssoRes.codigo, err);

            }
            catch (Exception ex)
            {
                log.Level = Constants.level_ERROR;
                var Error = new ErrorResponse { codigo = Constants.Error_code, message = ex.Message };
                log.Response = JsonSerializer.Serialize(Error);
                log.Message = ex.Message;
                log.InnerException = ex.InnerException.Message;
                return StatusCode(Constants.Error_code, Error);
            }
            finally
            {
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion
    }
}
