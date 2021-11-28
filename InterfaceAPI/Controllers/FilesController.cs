using Core.ServiceApp.Helpers;
using Core.ServiceApp.Utils.UtilContracts;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.Helpers;
using External.ServiceApp.DTOs;
using External.ServiceApp.Helpers;
using External.ServiceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterfaceApi.Controllers
{
    [Route("files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ILogUtils _logservice;

        public FilesController(ILogUtils logservice)
        {
            _logservice = logservice;
        }

        #region Save Alba-Keneth Files
        #region comments, swagger info
        /// <summary>
        /// Guarda los documentos y oficios de las alertas Alba-Keneth
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("albakeneth/docs")]
        public async Task<ActionResult> SaveAKDoc([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - /files/albakeneth/docs";
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

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.FILESAK_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                    foreach (IFormFile file in files)
                    {
                        string filePath = "";
                        if (file.Name == Constants.Oficio) 
                        {
                            filePath = Path.Combine(GlobalsVariables.AKOficios_Path, file.FileName);
                        }
                        else
                        {
                            filePath = Path.Combine(GlobalsVariables.AKDocs_Path, file.FileName);
                        }
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    return Ok();
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

        #region Save Alba-Keneth Pictures
        #region comments, swagger info
        /// <summary>
        /// Guarda las fotos y boletines de las alertas Alba-Keneth
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("albakeneth/fotos")]
        public async Task<ActionResult> SaveAKFotos([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - /files/albakeneth/fotos";
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.FILESAK_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                    foreach (IFormFile file in files)
                    {
                        string filePath = Path.Combine(GlobalsVariables.AKPics_Path, file.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }

                    return Ok();
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

        #region Save Isabel-Claudina Files
        #region comments, swagger info
        /// <summary>
        /// Guarda los documentos y oficios de las alertas Isabel-Claudina
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("isabelclaudina/docs")]
        public async Task<ActionResult> SaveICDoc([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - /files/isabelclaudina/docs";
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.FILESIC_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                    foreach (IFormFile file in files)
                    {
                        string filePath = Path.Combine(GlobalsVariables.ICDocs_Path, file.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }

                    return Ok();
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

        #region Save Isabel-Claudina Pictures
        #region comments, swagger info
        /// <summary>
        /// Guarda las fotos y los boletines de las alertas Isabel-Claudina
        /// </summary>
        /// <param name="SessionToken">Token de sesion</param>
        /// <returns>mensaje con el resultado del proceso</returns>
        /// <response code="200">Ejecucion exitosa</response>
        /// <response code="401">Sesion expirada</response>
        /// <response code="403">No autorizado</response>
        /// <response code="500">Error al ejecutar</response>
        #endregion
        [HttpPost]
        [Route("isabelclaudina/fotos")]
        public async Task<ActionResult> SaveICFotos([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "POST - /files/isabelclaudina/fotos";
            try
            {
                if (String.IsNullOrEmpty(SessionToken))
                {
                    var tokenError = new ErrorResponse { codigo = Constants.BadRequest_code, message = Constants.BadRequest_msj };
                    log.Response = JsonSerializer.Serialize(tokenError);
                    log.Level = Constants.level_WARN;
                    return StatusCode(Constants.BadRequest_code, tokenError);
                }

                SSOResponse ssoRes = SSOService.IsValidPermition(SessionToken, PermissionList.FILESIC_PERM).Result;
                if (ssoRes.codigo == Constants.OK_code)
                {
                    var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                    foreach (IFormFile file in files)
                    {
                        string filePath = Path.Combine(GlobalsVariables.ICPics_Path, file.FileName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                           await file.CopyToAsync(fileStream);
                        }
                    }

                    return Ok();
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
    }
}
