using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.Utils.UtilContracts;
using DataAccess.Entities.SICM_DbEntities.Sistema;
using DataAccess.Helpers;
using External.ServiceApp.DTOs;
using External.ServiceApp.Helpers;
using External.ServiceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterfaceApi.Controllers
{
    [Route("reportesAK")]
    [ApiController]
    public class AK_ReportesController : ControllerBase
    {
        private readonly ILogUtils _logservice;
        private readonly IResportesService _resportesService;

        public AK_ReportesController(ILogUtils logservice, IResportesService reportesService)
        {
            _logservice = logservice;
            _resportesService = reportesService;
        }

        [HttpGet]
        [Route("estatusAnio")]
        public async Task<ActionResult> Reporte([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - reportesAK/estatusAnio";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REPORTES_AK);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _resportesService.getReporteEstatuspoAño();
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

        [HttpGet]
        [Route("alertarPais")]
        public async Task<ActionResult> ReporteAlertasPorPais([FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - reportesAK/estatusAnio";
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REPORTES_AK);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = new Response();
                    servRes.data = new List<string>();
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

        [HttpGet]
        [Route("activadasMes/{anio}")]
        public async Task<ActionResult> ReporteAlertasActivadasPorMes([FromRoute] int anio, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - reportesAK/activadasMes/" + anio;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REPORTES_AK);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _resportesService.getReporteBoletinesPorMes(anio, 2);
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

        [HttpGet]
        [Route("desactivadasMes/{anio}")]
        public async Task<ActionResult> ReporteAlertasDesactivadasPorMes([FromQuery] int anio, [FromHeader] string SessionToken)
        {
            SICM_SYSLOG log = new SICM_SYSLOG();
            log.Url = "GET - reportesAK/desactivadasMes/" + anio;
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

                SSOResponse ssoRes = await SSOService.IsValidPermition(SessionToken, PermissionList.REPORTES_AK);
                if (ssoRes.codigo == Constants.OK_code)
                {
                    log.UserName = GlobalsVariables.UserID;
                    Response servRes = await _resportesService.getReporteBoletinesPorMes(anio, 3);
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
    }
}
