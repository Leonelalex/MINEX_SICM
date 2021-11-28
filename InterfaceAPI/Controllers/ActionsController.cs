using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.Utils.UtilContracts;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.Helpers;
using External.ServiceApp.DTOs;
using External.ServiceApp.Helpers;
using External.ServiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterfaceApi.Controllers
{
    [Route("accion")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IActionService _actionsService;
        private readonly ILogUtils _logservice;


        public ActionsController(IActionService actionsService, ILogUtils logservice)
        {
            _actionsService = actionsService;
            _logservice = logservice;
        }

        #region Notificar Autoridades
        #region comments, swagger info
        /// <summary>
        /// Envia notificacion vía correo a todas las misisones y correos adicionales ligados a la alerta
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAlerta">codigo de la alerta</param>
        /// <param name="model">Notificacion a enviar</param>
        /// <returns>Un mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("notificar/{codAlerta}")]
        public async Task<ActionResult> Notificar(NotificarAutoridades_DTO model, [FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Request = JsonSerializer.Serialize(model);
            log.Url = "PUT - accion/notificar/" + codAlerta;
            log.IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {

                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                if (String.IsNullOrEmpty(model.comentarios))
                    return StatusCode(400, new ErrorResponse { codigo = Constants.BadRequest_code, message = "Se requiere el ingreso de comentarios" });

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.NOTIFICAR_PERM);
                    if (ssoRes.codigo == Constants.OK_code)
                    {
                        log.UserName = GlobalsVariables.UserID;
                        Response servRes = await _actionsService.notificarAutoridades(model, codAlerta);
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
                var res = new ErrorResponse { codigo = 500, message = ex.Message };
                log.Level = Constants.level_ERROR;
                log.Exception = ex.Message;
                log.InnerException = ex.InnerException.Message;
                log.StackTrace = ex.StackTrace;
                log.Response = JsonSerializer.Serialize(res);
                return StatusCode(Constants.Error_code, res);
            }
            finally
            {
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Avistamiento
        #region comments, swagger info
        /// <summary>
        /// Envia una notificacion de avistamiento a todas las misiones y correos adiciones aosciados a alerta
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codBoletin">codigo del Boletin</param>
        /// <param name="model">Avistamiento a enviar</param>
        /// <returns>Un mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("avistamiento/{codBoletin}")]
        public async Task<ActionResult> Avistamiento(Avistamiento_DTO model, [FromRoute] int codBoletin, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Request = JsonSerializer.Serialize(model);
            log.Url = "PUT - accion/avistamiento/" + codBoletin;
            log.IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                if (String.IsNullOrEmpty(model.comentarios))
                    return StatusCode(Constants.BadRequest_code, new ErrorResponse { codigo = Constants.BadRequest_code, message = "Se requiere el ingreso de comentarios" });

                if (String.IsNullOrEmpty(model.direccion))
                    return StatusCode(Constants.BadRequest_code, new ErrorResponse { codigo = Constants.BadRequest_code, message = "Se requiere el ingreso de una dirección" });

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.AVISTAMIENTO_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _actionsService.avistamiento(model, codBoletin);
                    if (servRes.codigo == Constants.OK_code)
                    {
                        log.Level = Constants.level_INFO;
                        log.Response = JsonSerializer.Serialize(servRes.data);
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
                var res = new ErrorResponse { codigo = 500, message = ex.Message };
                log.Level = "ERROR";
                log.Exception = ex.Message;
                log.InnerException = ex.InnerException.Message;
                log.StackTrace = ex.StackTrace;
                log.Response = JsonSerializer.Serialize(res);
                return StatusCode(500, new ErrorResponse { codigo = 500, message = ex.Message });
            }
            finally
            {
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Cambio de Situacion
        #region comments, swagger info
        /// <summary>
        /// Envia una notificacion de cambio de situacion a todas las misiones y correos adiciones aosciados a alerta
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codBoletin">codigo del Boletin</param>
        /// <param name="model">Avistamiento a enviar</param>
        /// <returns>Un mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("situacion/{codBoletin}")]
        public async Task<ActionResult> CambioSituacion(CambioSituacion_DTO model, [FromRoute] int codBoletin, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Request = JsonSerializer.Serialize(model);
            log.Url = "PUT - accion/situacion/" + codBoletin;
            log.IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                if (String.IsNullOrEmpty(model.comentarios))
                    return StatusCode(Constants.BadRequest_code, new ErrorResponse { codigo = Constants.BadRequest_code, message = "Se requiere el ingreso de comentarios" });

                if (model.situacion == 0 && model.estatus == 0)
                    return StatusCode(Constants.BadRequest_code, new ErrorResponse { codigo = Constants.BadRequest_code, message = "Se requiere el cambio de la situación  o estado" });

                SSOResponse ssoRes = await SSOService .IsValidPermition(SessionToken, PermissionList.ESTATUS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _actionsService.cambioSituacion(model, codBoletin);
                    if (servRes.codigo == 200)
                    {
                        log.Level = Constants.level_INFO;
                        log.Response = JsonSerializer.Serialize(servRes.data);
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

        #region Ultimas Acciones
        #region comments, swagger info
        /// <summary>
        /// Obtiene las ultimas acciones registradas por el tipo de alerta
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="filter">parametros de paginacion</param>
        /// <param name="model">filtros de busqueda</param>
        /// <returns>Un listado con las ultimas acciones segun los parametros recibidos</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("ultimas")]
        public async Task<ActionResult> GetUltimasAcciones(UltimasAcciones_DTO model, [FromQuery] PaginationFilter filter, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - accion/ultimas";
            log.IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ULTIMASACC_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _actionsService.GetUltimasAcciones(model, filter);
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
