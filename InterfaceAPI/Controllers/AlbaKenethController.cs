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
    [Route("albakeneth")]
    [ApiController]
    public class AlbaKenethController : ControllerBase
    {
        private readonly IAlbaKenethService _albaKenethService;
        private readonly ILogUtils _logservice;

        public AlbaKenethController(IAlbaKenethService service, ILogUtils logservice)
        {
            _albaKenethService = service;
            _logservice = logservice;
        }

        #region Metodos de Alertas

        #region Crear Alerta
        #region comments, swagger info
        /// <summary>
        /// Crea una alerta Alba-Keneth
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="model">Alerta a registrar</param>
        /// <returns>Un mensaje con el resultado del proceso y el Id de la alerta registrada</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("alerta")]
        public async Task<ActionResult> CrearAlerta(AlertaAlbaKeneth_DTO model, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - /albakeneth/alerta";
            log.Request = JsonSerializer.Serialize(model);
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.CrearAlerta(model);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Get All Alertas
        #region comments, swagger info
        /// <summary>
        /// Retorna todas alertas Alba-Keneth Paginadas
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="filter">parametros de paginacion</param>
        /// <returns>Un listado con las alertas de la pagina recivida</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAll([FromQuery] PaginationFilter filter, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - /albakeneth/alerta";
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



                SSOResponse ssoRes = await SSOService .IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.GetAllAlerts(filter);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Get Alerta By Filters
        #region comments, swagger info
        /// <summary>
        /// Retorna todas alertas Alba-Keneth Paginadas y filtradas
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="pagefilter">parametros de paginacion</param>
        /// <param name="filters">filtros de busqueda</param>
        /// <returns>Un listado con las alertas de la pagina luego de filtrarlas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("alertas")]
        public async Task<ActionResult> GetByFilters(AlertSearchFilters filters, [FromQuery] PaginationFilter pagefilter, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - /albakeneth/alertas";
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



                SSOResponse ssoRes = await SSOService .IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.GetAlertsByFilters(filters, pagefilter);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Get Bitacora de Alertas
        #region comments, swagger info
        /// <summary>
        /// Retorna la bitacora de la Alerta 
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAlerta">codigo de la Alerta</param>
        /// <returns>Un listado todos los registros de bitacora de la alerta recibida</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("bitacora/{codAlerta}")]
        public async Task<ActionResult> GetBitacora([FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - albakeneth/bitacora/" + codAlerta;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.GetBitacora(codAlerta);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Update Alerta
        #region comments, swagger info
        /// <summary>
        /// Edita la alerta Alba-Keneth
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAlerta">codigo de la Alerta</param>
        /// <param name="model">campos de la alerta a editar</param>
        /// <returns>Un mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("alerta/{codAlerta}")]
        public async Task<ActionResult> UpdateAlerta(AlertaAlbaKeneth_DTO model, [FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - /albakeneth/alerta/" + codAlerta;
            log.Request = JsonSerializer.Serialize(model);
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



                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.UpdateAlerta(model, codAlerta);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Desactivar Alerta
        #region comments, swagger info
        /// <summary>
        /// Desactiva la Alerta Alba-Keneth
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAlerta">codigo de la Alerta</param>
        /// <param name="model">campos de la alerta a editar</param>
        /// <returns>Un mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("desactivar/{codAlerta}")]
        public async Task<ActionResult> DesactivarAlerta(DesactivarAlerta_AK_DTO model, [FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - /albakeneth/desactivar/" + codAlerta;
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



                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.DesactivarAlerta(model, codAlerta);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #endregion

        #region Metodos de Boletines

        #region Actualizar Boletin
        #region comments, swagger info
        /// <summary>
        /// Edita el boletin de una alerta Alba-Keneth
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codBoletin">codigo del Boletin</param>
        /// <param name="boletin">campos del boletin a editar</param>
        /// <returns>Un mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("boletin/{codBoletin}")]
        public async Task<ActionResult> UpdateBoletin(BoletinAK_DTO boletin, [FromRoute] int codBoletin, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - /albakeneth/boletin/" + codBoletin;
            log.Request = JsonSerializer.Serialize(boletin);
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



                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.EditBoletin(boletin, codBoletin);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Agregar Boletin 
        #region comments, swagger info
        /// <summary>
        /// Agregar un Boletin a la Alerta
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAlerta">codigo de la Alerta</param>
        /// <param name="boletin">boletin a crear</param>
        /// <returns>Un mensaje con el resultado del proceso y el codigo del boletin registrado</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("boletin/{codAlerta}")]
        public async Task<ActionResult> PostBoletin(BoletinAK_DTO boletin, [FromRoute] int codAlerta, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Logged = DateTime.Now;
            log.Url = "POST - /albakeneth/boletin/" + codAlerta;
            log.Request = JsonSerializer.Serialize(boletin);
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = _albaKenethService.AddBoletin(boletin, codAlerta).Result;
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
             
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #region Eliminar Boletin
        #region comments, swagger info
        /// <summary>
        /// Elimina un boletin de la Alerta
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codBoletin">codigo del Boletin</param>
        /// <returns>Un mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpDelete]
        [Route("boletin/{codBoletin}")]
        public async Task<ActionResult> DeleteBoletin(int codBoletin, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "DELETE - /albakeneth/boletin/" + codBoletin;
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



                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REGISTRARAK_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _albaKenethService.DeleteBoletin(codBoletin);
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
                log.Logged = DateTime.Now;
                await _logservice.SaveLogError(log);
            }
        }
        #endregion

        #endregion


    }
}
