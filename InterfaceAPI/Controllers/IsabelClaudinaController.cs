using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.DTO.Core_DTOs.Responses;
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

namespace InterfaceApi.Controllers
{
    [Route("isabelclaudina")]
    [ApiController]
    public class IsabelClaudinaController : ControllerBase
    {
        private readonly IIsabelClaudinaService _isabelClaudinaService;
        private readonly ILogUtils _logservice;

        public IsabelClaudinaController(IIsabelClaudinaService service, ILogUtils logservice)
        {
            _isabelClaudinaService = service;
            _logservice = logservice;
        }

        #region Get All Alertas
        #region comments, swagger info
        /// <summary>
        /// Retorna todas las alertas Isabel-Claudina
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="SessionToken"></param>
        /// <returns>Un listado con todas la alertas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("all")]
        public ActionResult GetAll([FromQuery] PaginationFilter filter, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - isabelclaudina/all";
            log.Request = JsonSerializer.Serialize(filter);

            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Request = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                if (!ModelState.IsValid)
                {
                    log.Response = JsonSerializer.Serialize(filter);
                    log.Level = Constants.level_WARN;
                    return StatusCode(400, new ErrorResponse { codigo = 400, message = Constants.BadRequest_msj });
                }

                SSOResponse ssoRes =  SSOService.IsValidPermition(SessionToken, PermissionList.VER_ALERTASIC_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserName;
                    Response servRes = _isabelClaudinaService.GetAll(filter).Result;
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
             _logservice.SaveLogError(log);
            }

        }
        #endregion

        #region Get Alerta By Filters
        #region comments, swagger info
        /// <summary>
        /// Retorna todas las alertas Isabel-Claudina por filtros
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pagefilter"></param>
        /// <param name="SessionToken"></param>
        /// <returns>Un listado con todas la alertas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("alertas")]
        public ActionResult GetByFilters(AlertSearchFilters filters, [FromQuery] PaginationFilter pagefilter, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - isabelclaudina/all";
            log.Request = JsonSerializer.Serialize(filters);

            try
            {
                if (filters == null)
                    filters = new AlertSearchFilters();

                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Request = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.VER_ALERTASIC_PERM).Result;
                if (ssoRes.codigo == 200)
                {
                    log.UserName = GlobalsVariables.UserName;
                    Response servRes = _isabelClaudinaService.GetAlertsByFilters(filters, pagefilter).Result;
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
                _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Get Bitacora IC
        #region comments, swagger info
        /// <summary>
        /// Retorna La bitacora de una alerta Isabel-Claudina
        /// </summary>
        /// <param name="codAlerta"></param>
        /// <param name="SessionToken"></param>
        /// <returns>Un listado de la bitacora de la alerta seleccionada</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("bitacora/{codAlerta}")]
        public ActionResult getBitacora([FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - isabelclaudina/bitacora/" + codAlerta;

            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Request = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                if (!ModelState.IsValid)
                {
                    log.Response = Constants.BadRequest_msj;
                    log.Level = Constants.level_WARN;
                    return StatusCode(400, new ErrorResponse { codigo = 400, message = Constants.BadRequest_msj });
                }

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.BITACORAIC_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserName;
                    Response servRes = _isabelClaudinaService.GetBitacora(codAlerta).Result;

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
                _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region registrar alerta
        #region comments, swagger info
        /// <summary>
        /// Registra una alerta Isabel-Claudina
        /// </summary>
        /// <param name="alerta"></param>
        /// <returns>Un listado con todas la alertas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("registrar")]
        public ActionResult registrarAlerta(AlertaIsabelClaudina_DTO alerta)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - isabelclaudina/registrar";
            log.Request = JsonSerializer.Serialize(alerta);
            log.UserName = "Ministerio Publico";

            AlertaIsabelClaudina_ViewModel res = new AlertaIsabelClaudina_ViewModel();
            res.respuesta = new RespuestaMP_ViewModel { institucionOrigen = Constants.InstitutionMinex, recepcionRespuesta = true };
            res.responseTime = DateTime.Now;

            try
            {
                if (!ModelState.IsValid)
                {
                    res.respuesta.recepcionRespuesta = false;
                    res.idError = Constants.BadRequest_code;
                    res.message = Constants.NoCreateAlert;
                    res.ErrorMessage = Constants.BadModelState;

                    log.Response = Constants.BadRequest_msj;
                    log.Level = Constants.level_WARN;

                    return StatusCode(Constants.BadRequest_code, res);
                }

                res = _isabelClaudinaService.CreateAlert(alerta).Result;
              
                if (res.idError == Constants.OK_code)
                {
                    log.Level = Constants.level_INFO;
                    log.Response = JsonSerializer.Serialize(res);
                    log.Message = JsonSerializer.Serialize(res.message);
                    return Ok(res);

                }else
                {
                    log.Level = Constants.level_ERROR;
                    var serverError = new ErrorResponse { codigo = res.idError, message = res.message };
                    log.Response = JsonSerializer.Serialize(serverError);
                    log.Message = res.ErrorMessage;
                    return StatusCode(Constants.Error_code, res);
                }
            }
            catch (Exception ex)
            {
                res.respuesta.recepcionRespuesta = false;
                res.idError = Constants.Error_code;
                res.message = Constants.NoCreateAlert;
                res.ErrorMessage = ex.InnerException.Message;

                log.Level = Constants.level_ERROR;
                var serverError = new ErrorResponse { codigo = res.idError, message = res.message };
                log.Response = JsonSerializer.Serialize(serverError);
                log.Message = res.message;
                log.InnerException = ex.InnerException.Message;

                return StatusCode(Constants.Error_code, res);
            }
            finally
            {
                _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region activar alerta
        #region comments, swagger info
        /// <summary>
        /// Activa una alerta Isabel-Claudina
        /// </summary>
        /// <param name="misiones"></param>
        /// <param name="codAlerta"></param>
        /// <param name="SessionToken"></param>
        /// <returns>Un listado con todas la alertas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("activar/{codAlerta}")]
        public ActionResult activarAlerta(Activar_IC_DTO misiones, [FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - isabelclaudina/activar/" + codAlerta;
            log.Request = JsonSerializer.Serialize(misiones);

            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Request = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                if (misiones.misiones.Count == 0 && misiones.difusionInternacional == false)
                    return StatusCode(400, new ErrorResponse { codigo = 400, message = "Debe seleccionar misiones o difusión internacional" });

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.ACTIVARIC_PERM).Result;
                if (ssoRes.codigo == 200)
                {
                    log.UserName = GlobalsVariables.UserName;
                    Response servRes = _isabelClaudinaService.ActivateAlert(misiones, codAlerta).Result;

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
                _logservice.SaveLogError(log);
            }

        }
        #endregion

        #region desactivar alerta isabel-Claudina
        #region comments, swagger info
        /// <summary>
        /// Desactiva una alerta Isabel-Claudina
        /// </summary>
        /// <param name="alerta"></param>
        /// <returns>Un listado con todas la alertas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("desactivar")]
        public ActionResult desactiveAlert(AlertaIsaChangeStatusDTO alerta)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - isabelclaudina/desactivar";
            log.Request = JsonSerializer.Serialize(alerta);
            log.UserName = "Ministerio Publico";

            AlertaIsabelClaudina_ViewModel res = new AlertaIsabelClaudina_ViewModel();
            res.respuesta = new RespuestaMP_ViewModel { institucionOrigen = Constants.InstitutionMinex, recepcionRespuesta = true };
            res.responseTime = DateTime.Now;

            try
            {
                if (!ModelState.IsValid)
                {
                    res.respuesta.recepcionRespuesta = false;
                    res.idError = 400;
                    res.message = Constants.NoCreateAlert;
                    res.ErrorMessage = Constants.BadModelState;

                    log.Response = JsonSerializer.Serialize(alerta);
                    log.Level = Constants.level_WARN;
                    return StatusCode(400, new ErrorResponse { codigo = 400, message = Constants.BadRequest_msj });
                }

                res = _isabelClaudinaService.DesactiveAlert(alerta).Result;
                if (res.idError == Constants.OK_code)
                {
                    log.Level = Constants.level_INFO;
                    log.Response = JsonSerializer.Serialize(alerta);
                    log.Message = JsonSerializer.Serialize(res.message);
                    return Ok(res);

                }
                else
                {
                    log.Level = Constants.level_ERROR;
                    var serverError = new ErrorResponse { codigo = res.idError, message = res.message };
                    log.Response = JsonSerializer.Serialize(serverError);
                    log.Message = res.ErrorMessage;
                    return StatusCode(Constants.Error_code, res);
                }

            }
            catch (Exception ex)
            {
                res.respuesta.recepcionRespuesta = false;
                res.idError = 500;
                res.message = Constants.NoCreateAlert;
                res.ErrorMessage = ex.Message;

                log.Level = Constants.level_ERROR;
                var serverError = new ErrorResponse { codigo = res.idError, message = res.message };
                log.Response = JsonSerializer.Serialize(serverError);
                log.Message = res.message;
                log.InnerException = ex.InnerException.Message;

                return StatusCode(Constants.Error_code, res);
            }
            finally
            {
                _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Difundir alerta
        #region comments, swagger info
        /// <summary>
        /// Difundir una alerta Isabel-Claudina
        /// </summary>
        /// <param name="model"></param>
        /// <param name="codAlerta"></param>
        /// <param name="SessionToken"></param>
        /// <returns>Un listado con todas la alertas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("difundir/{codAlerta}")]
        public ActionResult difundir(DiffusionIC_DTO model, [FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - isabelclaudina/difundir/" + codAlerta;
            log.Request = JsonSerializer.Serialize(model);

            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Request = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                if (!ModelState.IsValid)
                {
                    log.Response = Constants.BadRequest_msj;
                    log.Level = Constants.level_WARN;
                    return StatusCode(400, new ErrorResponse { codigo = 400, message = Constants.BadRequest_msj });
                }

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.DIFUNDIRIC_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserName;
                    Response servRes = _isabelClaudinaService.diffusionIC(model, codAlerta).Result;
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
                _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Alertar por activar
        #region comments, swagger info
        /// <summary>
        /// Búsquedas de alerta por activar Isabel-Claudina
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="SessionToken"></param>
        /// <returns>Un listado con todas la alertas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("alerta/porActivar")]
        public ActionResult getAlertaSinActivar([FromQuery] PaginationFilter filter, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - isabelclaudina/alerta/porActivar";
            log.Request = JsonSerializer.Serialize(filter);

            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                { 
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Request = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.VER_ALERTASIC_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserName;
                    Response servRes = _isabelClaudinaService.GetAlertasSinActivar(filter).Result;
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
                _logservice.SaveLogError(log);
            }
        }
        #endregion
    }
}
