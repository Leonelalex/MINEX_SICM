using Core.ServiceApp.DTO.Core_DTOs;
using Core.ServiceApp.DTO.Core_DTOs.Catalogos_DTOS;
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
    
    [Route("catalogos")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly ICatalogosService _catalogosService;
        private readonly ILogUtils _logservice;

        public CatalogosController(ICatalogosService catalogosService, ILogUtils logservice)
        {
            _catalogosService = catalogosService;
            _logservice = logservice;
        }

        #region GET All Catalogos
        #region comments, swagger info
        /// <summary>
        /// Retorna todos los catalogos de la aplicacion
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>Un listado con todos los catalogos con sus respectivos registros disponibles</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("catalogos")]
        public async Task<ActionResult> Catalogos([FromHeader] string SessionToken)
        {
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.GETCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    Response servRes = await _catalogosService.GetAllCatalogos();
                    if (servRes.codigo == Constants.OK_code)
                    {
                        Response.Headers.Add(Constants.SessionToken, ssoRes.newToken);
                        return Ok(servRes.data);
                    }
                    else
                    {
                        var serverError = new ErrorResponse { codigo = servRes.codigo, message = servRes.message };
                        return StatusCode(servRes.codigo, serverError);
                    }
                }

                var err = new ErrorResponse { codigo = ssoRes.codigo, message = Constants.NoAutorized_msj };
                return StatusCode(ssoRes.codigo, err);

            }
            catch (Exception ex)
            {
                var Error = new ErrorResponse { codigo = Constants.Error_code, message = ex.Message };
                return StatusCode(Constants.Error_code, Error);
            }
        }

        #endregion

        #region CRUD Catalogo Situacion

        #region Get Catalogo Situacion
        #region comments, swagger info
        /// <summary>
        /// Retorna todos los registros del catalogo de situacion
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>Un listado con todas las situaciones especiales registradas</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("situacion/all")]
        public async Task<ActionResult> GetSituacion([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - /catalogos/situacion/all";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.GETCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.GetAllSituacion();
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

        #region Put Catalogo Situacion
        #region comments, swagger info
        /// <summary>
        /// Edita una situacion especial
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="model">campos de situacion especial a actualizar</param>
        /// <param name="codSituacion">codigo de la situacion especial</param>
        /// <returns>mensaje con el resultado de la operacion</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("situacion/{codSituacion}")]
        public async Task<ActionResult> PutSituacion(CatalogoGenerico_DTO model, [FromRoute] int codSituacion, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - catalogos/situacion/" + codSituacion;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.PutSituacion(model, codSituacion);
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

        #region Post Catalogo Situacion
        #region comments, swagger info
        /// <summary>
        /// Crea una nueva situacion especial
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="model">campos de situacion especial nueva</param>
        /// <returns>mensaje con el resultado de la operacion y el codigo del la nueva situacion especial registrada</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("situacion")]
        public async Task<ActionResult> PostSituacion(CatalogoGenerico_DTO model, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - /catalogos/situacion/";
            log.Request = JsonSerializer.Serialize(model);
            log.IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.PostSituacion(model);
                    if (servRes.codigo == Constants.OK_code)
                    {
                        log.Level = Constants.level_INFO;
                        Response.Headers.Add(Constants.SessionToken, ssoRes.newToken);
                        return Ok(servRes.data);
                    }
                    else
                    {
                        var serverError = new ErrorResponse { codigo = servRes.codigo, message = servRes.message };
                        log.Response = JsonSerializer.Serialize(serverError);
                        log.Message = servRes.message;
                        log.InnerException = servRes.innerError;
                        return StatusCode(servRes.codigo, serverError);
                    }
                }

                var err = new ErrorResponse { codigo = ssoRes.codigo, message = Constants.NoAutorized_msj };
                log.Response = JsonSerializer.Serialize(err);
                return StatusCode(ssoRes.codigo, err);

            }
            catch (Exception ex)
            {
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

        #region Delete Catalogo Situacion
        #region comments, swagger info
        /// <summary>
        /// Desactiva una situacion especial
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codSituacion">codigo de la situacion especial</param>
        /// <returns>mensaje con el resultado de la operacion</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpDelete]
        [Route("situacion/{codSituacion}")]
        public async Task<ActionResult> DeleteSituacion([FromRoute] int codSituacion, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "DELETE - catalogos/situacion/" + codSituacion;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.DeleteSituacion(codSituacion);
                    if (servRes.codigo == Constants.OK_code)
                    {
                        log.Level = Constants.level_INFO;
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

        #endregion

        #region CRUD Catalogo Estatus

        #region Get Catalogo Estatus
        #region comments, swagger info
        /// <summary>
        /// Retorna todos los registros del catalogo de estatus
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>listado con todos los registrs del catalogo de estatus</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("estatus/all")]
        public async Task<ActionResult> GetEstatus([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - catalogos/estatus/all";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.GETCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.GetAllEstatus();
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

        #region Put Catalogo Estatus
        #region comments, swagger info
        /// <summary>
        /// Edita un registro de estatus
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="model">campos del estatus a actualizar</param>
        /// <param name="codEstatus">codigo de estatus</param>
        /// <returns>listado con todos los registrs del catalogo de estatus</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("estatus/{codEstatus}")]
        public async Task<ActionResult> PutEstatus(CatalogoGenerico_DTO model, [FromRoute] int codEstatus, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - catalogos/estatus/" + codEstatus;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.PutEstatus(model, codEstatus);
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

        #region Post Catalogo Estatus
        #region comments, swagger info
        /// <summary>
        /// Crea un nuevo estatus en el catalogo
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="model">campos del estatus a actualizar</param>
        /// <returns>mensaje con el resultado del proceso y el codigo del nuevo registro</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("estatus")]
        public async Task<ActionResult> PostEstatus(CatalogoGenerico_DTO model, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - catalogos/estatus";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.PostEstatus(model);
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

        #region Delete Catalogo Estatus
        #region comments, swagger info
        /// <summary>
        /// Desactiva un registro de estatus
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codEstatus">codigo de estatus</param>
        /// <returns>mensaje del resultado de la ejecucion</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpDelete]
        [Route("estatus/{codEstatus}")]
        public async Task<ActionResult> DeleteEstatus([FromRoute] int codEstatus, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "DELETE - catalogos/estatus/" + codEstatus;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.DeleteEstatus(codEstatus);
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


        #endregion

        #region CRUD Catalogo Acciones Notificacion

        #region GET Acciones Notificacion
        #region comments, swagger info
        /// <summary>
        /// Retorna todos los registros del catalogo de acciones de notificacion
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>listado con todos los registrs del catalogo de acciones de notificacion</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("accion/all")]
        public async Task<ActionResult> GetAccionNotificacion([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - catalogos/accion/all";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.GETCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.GetAllAccionesNotificacion();
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

        #region Post Catalogo Estatus
        #region comments, swagger info
        /// <summary>
        /// Crea una nueva accion de notificacion
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="model">nueva accion de notificacion</param>
        /// <returns>mesnaje del resultado del proceso y el nuevo codigo de la accion registrada</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("accion")]
        public async Task<ActionResult> PostAccionNotificacion(CatalogoGenerico_DTO model, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - catalogos/accion";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.PostAccionNotificacion(model);
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

        #region Put Catalogo Estatus
        #region comments, swagger info
        /// <summary>
        /// Edita una accion de notificacion
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAccion">codigo de la accion</param>
        /// <param name="model">campos a editar</param>
        /// <returns>mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("accion/{codAccion}")]
        public async Task<ActionResult> PutAccionNotificacion(CatalogoGenerico_DTO model, [FromRoute] int codAccion, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - catalogos/accion/" + codAccion;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.PutAccionNotificacion(model, codAccion);
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

        #region Delete Catalogo Estatus
        #region comments, swagger info
        /// <summary>
        /// Desactiva una accion de notificacion
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codAccion">codigo de la accion</param>
        /// <returns>mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpDelete]
        [Route("accion/{codAccion}")]
        public async Task<ActionResult> DeleteAccionNotificacion([FromRoute] int codAccion, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "DELETE - catalogos/accion/" + codAccion;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.DeleteAccionNotificacion(codAccion);
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

        #endregion

        #region CRUD Paramaetros Generales

        #region Get Parametros
        #region comments, swagger info
        /// <summary>
        /// Retorna todos los parametros del sistema
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>listado con los parametros del sistema</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpGet]
        [Route("param/all")]
        public async Task<ActionResult> GetParametrosGenerales([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - param/all";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.GETCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.GetAllParametros();
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

        #region PUT Parametros Generales
        #region comments, swagger info
        /// <summary>
        /// Edita el valor de los parametros del sistema
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <param name="codParam">codigo del parametro</param>
        /// <param name="model">campos a editar</param>
        /// <returns>mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="400">Request erroneo</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPut]
        [Route("param/{codParam}")]
        public async Task<ActionResult> PutAccionNotificacion(ParametrosGenerales_DTO model, [FromRoute] int codParam, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "PUT - catalogos/param/" + codParam;
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

                if (String.IsNullOrEmpty(model.valor))
                {
                    var error = new ErrorResponse { codigo = Constants.BadRequest_code, message = "El valor del parametro es invalido" };
                    log.Response = JsonSerializer.Serialize(error);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, error);
                }                        

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.ADMINCATALOGOS_PERM);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _catalogosService.PutParametrosGenerales(model, codParam);
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

        #endregion
    }
}
