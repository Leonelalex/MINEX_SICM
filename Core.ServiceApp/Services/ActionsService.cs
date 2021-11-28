using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.Helpers.ViewModels;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using External.ServiceApp.DTOs;
using External.ServiceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services
{
    public class ActionsService : IActionService
    {
        private readonly ISICM_ALERTAS_REPO _alertasRepo;
        private readonly ISICM_BITACORA_ALERTA_REPO _bitacoraRepo;
        private readonly IEmailService _emailService;
        private readonly ISICM_ACCIONES_ALERTA_REPO _accionesAlertaRepo;
        private readonly ISICM_BOLETINES_REPO _boletinesRepo;
        private readonly ISICM_ACCIONES_NOTIFICACION_REPO _accionesNotificacion;
        private readonly ISICM_ESTATUS_ALERTA_REPO _estatusRepo;
        private readonly ISICM_SITUACION_ALERTA_REPO _situacionRepo;

        public ActionsService(ISICM_ALERTAS_REPO alertasRepo, ISICM_BITACORA_ALERTA_REPO bitacoraRepo, IEmailService emailService, 
            ISICM_ACCIONES_ALERTA_REPO accionesAlertaRepo, ISICM_BOLETINES_REPO boletinesRepo, ISICM_ACCIONES_NOTIFICACION_REPO accionesNotificacion,
            ISICM_ESTATUS_ALERTA_REPO estatusRepo, ISICM_SITUACION_ALERTA_REPO situacionRepo)
        {
            _alertasRepo = alertasRepo;
            _bitacoraRepo = bitacoraRepo;
            _emailService = emailService;
            _accionesAlertaRepo = accionesAlertaRepo;
            _boletinesRepo = boletinesRepo;
            _accionesNotificacion = accionesNotificacion;
            _estatusRepo = estatusRepo;
            _situacionRepo = situacionRepo;

        }

        #region Notificar Autoridades
        public async Task<Response> notificarAutoridades(NotificarAutoridades_DTO model, int codAlerta)
        {
            Response res = new Response();
            try
            {
                SICM_ALERTAS alerta = await _alertasRepo.GetByID(codAlerta);
                if (alerta.estadoAlerta != Constants.CodeRegisterAlert)
                {
                    EmailModel email = new EmailModel();

                    SICM_ACCIONES_ALERTA accion = _accionesAlertaRepo.GetByID(4).Result;
                    string contenido = accion.ContenidoEmail;

                    email.numeroAlerta = alerta.codigoAlerta;

                    if (!String.IsNullOrEmpty(alerta.correosAdicionales))
                        email.listCorreos = alerta.correosAdicionales + ",";

                    if (!String.IsNullOrEmpty(model.correosAdicionales))
                        email.listCorreos += model.correosAdicionales;
                    else
                        email.listCorreos = email.listCorreos.Remove(email.listCorreos.Length - 1);


                    email.listDocs = model.adjuntos;
                    email.titulo = "Notificación Alerta " + alerta.codigoAlerta;
                    email.saludo = "Señores funcionarios: ";

                    SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();
                    string comentarios = "Acciones Tomadas : ";
                    if (model.acciones.Count > 0)
                    {
                        foreach (int acc in model.acciones) {
                            bitacora = new SICM_BITACORA_ALERTA();
                            bitacora.usuario = GlobalsVariables.UserName;
                            bitacora.pais = GlobalsVariables.Pais;
                            bitacora.codigoAlerta = codAlerta;
                            bitacora.codigoEstado = alerta.estadoAlerta;
                            bitacora.codigoAccion = accion.Codigo;
                            bitacora.codigoAccionNotificacion = acc;
                            bitacora.fecha = DateTime.Now;
                            bitacora.observaciones = model.comentarios;
                            bitacora.adjunto = model.adjuntos;
                            comentarios += _accionesNotificacion.GetByID(acc).Result.nombre + ",";
                            await _bitacoraRepo.Add(bitacora);
                        }
                        comentarios = comentarios.Remove(comentarios.Length - 1);
                    }
                    else
                    {
                        comentarios = "";
                        bitacora.usuario = GlobalsVariables.UserName;
                        bitacora.pais = GlobalsVariables.Pais;
                        bitacora.codigoAlerta = codAlerta;
                        bitacora.codigoEstado = alerta.estadoAlerta;
                        bitacora.codigoAccion = accion.Codigo;
                        bitacora.fecha = DateTime.Now;
                        bitacora.observaciones = model.comentarios;
                        bitacora.adjunto = model.adjuntos;

                        await _bitacoraRepo.Add(bitacora);
                    }


                    contenido = contenido.Replace("[observaciones]", comentarios + "<br>" + "Comentarios: " + model.comentarios);

                    if (alerta.tipoAlerta == 1)
                    {
                        contenido = contenido.Replace("[noAlerta]", "Alba-Keneth No." + alerta.codigoAlerta);
                        contenido = contenido.Replace("[noOficio]", alerta.codigoOficio);
                        contenido = contenido.Replace("[persona]", "del NNA");
                        contenido = contenido.Replace("[tipoAlerta]", "Alba-Keneth");
                        email.subjet = "[SICM] Notificación Alerta Alba-Keneth No." + alerta.codigoAlerta;

                        email.body = contenido;

                        await _emailService.sendActionEmailAK(email);
                    }
                    else
                    {
                        contenido = contenido.Replace("[noAlerta]", "Isabel-Claudina No." + alerta.codigoAlerta);
                        contenido = contenido.Replace("[noOficio]", alerta.codigoOficio);
                        contenido = contenido.Replace("[persona]", "de la mujer");
                        contenido = contenido.Replace("[tipoAlerta]", "Isabel-Claudina");
                        email.subjet = "[SICM] Notificación Alerta Isabel-Claudina No." + alerta.codigoAlerta;

                        email.body = contenido;

                        await _emailService.sendActionEmailIC(email);
                    }

                    contenido = contenido.Replace("[observaciones]", comentarios + "<br>" + "Comentarios: " +  model.comentarios);

                    res.codigo = Constants.OK_code;
                    res.data = new { message = "Notificaciones enviadas con éxito" };
                }
                else
                {
                    res.codigo = Constants.Error_code;
                    res.message = "No se ha difundido la alerta";
                }
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al enviar las notificaciones";
            }

            return res;
        }
        #endregion

        #region Avistamiento
        public async Task<Response> avistamiento (Avistamiento_DTO model, int codBoletin)
        {
            Response res = new Response();
            try
            {
                SICM_BOLETINES boletin = await _boletinesRepo.GetByID(codBoletin);
                SICM_ALERTAS alerta = await _alertasRepo.GetByID(boletin.codigoAlerta);

                if (alerta.estadoAlerta == Constants.CodeRegisterAlert) {
                    res.codigo = Constants.Error_code;
                    res.message = "No se ha difundido la alerta";
                    return res;
                }

                if (boletin != null)
                {
                    EmailModel email = new EmailModel();

                    SICM_ACCIONES_ALERTA accion = _accionesAlertaRepo.GetByID(11).Result;

                    string contenido = accion.ContenidoEmail;


                    contenido = contenido.Replace("[nombre]", boletin.primerNombre + " " + boletin.primerApellido);
                    contenido = contenido.Replace("[dir]", model.direccion);
                    contenido = contenido.Replace("[noOficio]", alerta.codigoOficio);
                    contenido = contenido.Replace("[observaciones]", "Comentarios: " + model.comentarios);

                    email.numeroAlerta = alerta.codigoAlerta;
                    email.datosAlerta = "";

                    if (alerta.correosAdicionales != null && alerta.correosAdicionales != "")
                        email.listCorreos = alerta.correosAdicionales + ",";

                    if (model.correosAdicionales != null && model.correosAdicionales != "")
                        email.listCorreos = email.listCorreos + model.correosAdicionales;
                    else
                        email.listCorreos = email.listCorreos.Remove(email.listCorreos.Length - 1);

                    email.listDocs = model.adjuntos;
                    email.titulo = "Avistamiento Alerta " + alerta.codigoAlerta;
                    email.saludo = "Señores funcionarios: ";
                    email.body = contenido;

                    if (alerta.tipoAlerta == 1)
                    {
                        contenido = contenido.Replace("[noAlerta]", "Alba-Keneth No." + alerta.codigoAlerta);
                        contenido = contenido.Replace("[persona]", "del NNA");
                        contenido = contenido.Replace("[tipoAlerta]", "Alba-Keneth");
                        email.subjet = "[SICM] Avistamiento Alerta Alba-Keneth No." + alerta.codigoAlerta;
                        email.body = contenido;

                        await _emailService.sendActionEmailAK(email);
                    }
                    else
                    {
                        contenido = contenido.Replace("[noAlerta]", "Isabel-Claudina No." + alerta.codigoAlerta);
                        contenido = contenido.Replace("[persona]", "de la mujer");
                        contenido = contenido.Replace("[tipoAlerta]", "Isabel Claudina");
                        email.subjet = "[SICM] Avistamiento Alerta Isabel-Claudina No." + alerta.codigoAlerta;
                        email.body = contenido;

                        await _emailService.sendActionEmailIC(email);
                    }

                    SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                    bitacora.usuario = GlobalsVariables.UserName;
                    bitacora.pais = GlobalsVariables.Pais;
                    bitacora.codigoAlerta = boletin.codigoAlerta;
                    bitacora.codigoEstado = alerta.estadoAlerta;
                    bitacora.codigoAccion = accion.Codigo;
                    bitacora.fecha = DateTime.Now;
                    bitacora.observaciones = model.comentarios + " Dirección: " + model.direccion;
                    bitacora.adjunto = model.adjuntos;

                    await _bitacoraRepo.Add(bitacora);

                    res.codigo = Constants.OK_code;
                    res.data = new { message = "Avistamiento notificado con éxito" };
                }
                else
                {
                    res.codigo = Constants.NoContent_code;
                    res.message = "No se encontró el código de boletín";
                }
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Cambio Situacion
        public async Task<Response> cambioSituacion (CambioSituacion_DTO model, int codBoletin)
        {
            Response res = new Response();
            try
            {
                SICM_BOLETINES boletin = await _boletinesRepo.GetByID(codBoletin);
                SICM_ALERTAS alerta = await _alertasRepo.GetByID(boletin.codigoAlerta);
                SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                if (alerta.estadoAlerta == Constants.CodeRegisterAlert)
                {
                    res.codigo = Constants.Error_code;
                    res.message = "No se ha difundido la alerta";
                    return res;
                }

                if (boletin != null)
                {
                    EmailModel email = new EmailModel();

                    bitacora.observaciones = boletin.primerNombre + " " + boletin.primerApellido;

                    if (model.estatus != 0)
                    {
                        string newEstatus = _estatusRepo.GetNameByIdAsync(model.estatus);
                        string oldEstatus = _estatusRepo.GetNameByIdAsync(boletin.codigoEstatus);
                        bitacora.observaciones += " cambio de estado \"" + oldEstatus + "\" a \"" + newEstatus + "\",";
                        boletin.codigoEstatus = model.estatus;
                    }
                    if (model.situacion != 0)
                    {
                        string newSituacion = _situacionRepo.GetNameByIdAsync(model.situacion);
                        string oldSituacion = _situacionRepo.GetNameByIdAsync(boletin.codigoSituacion);
                        bitacora.observaciones += " cambio de situación de \"" + oldSituacion + "\" a \"" + newSituacion + "\",";
                        boletin.codigoSituacion = model.situacion;
                    }


                    await _boletinesRepo.Update(boletin);

                    SICM_ACCIONES_ALERTA accion = _accionesAlertaRepo.GetByID(13).Result;

                    string contenido = accion.ContenidoEmail;


                    contenido = contenido.Replace("[nombre]", boletin.primerNombre + " " + boletin.primerApellido);
                    contenido = contenido.Replace("[noOficio]", alerta.codigoOficio);
                    contenido = contenido.Replace("[observaciones]", "Comentarios: " + model.comentarios);

                    email.numeroAlerta = alerta.codigoAlerta;
                    email.datosAlerta = "";
                    if (alerta.correosAdicionales != null && alerta.correosAdicionales != "")
                        email.listCorreos = alerta.correosAdicionales + ",";

                    if (model.correosAdicionales != null && model.correosAdicionales != "")
                        email.listCorreos = email.listCorreos + model.correosAdicionales;
                    else
                        email.listCorreos = email.listCorreos.Remove(email.listCorreos.Length - 1);

                    email.listDocs = model.adjuntos;
                    email.titulo = "Cambio de Estado/Situacion Alerta " + alerta.codigoAlerta;
                    email.saludo = "Señores funcionarios: ";

                    bitacora.usuario = GlobalsVariables.UserName;
                    bitacora.pais = GlobalsVariables.Pais;
                    bitacora.codigoAlerta = alerta.codigo;
                    bitacora.codigoEstado = alerta.estadoAlerta;
                    bitacora.codigoAccion = accion.Codigo;
                    bitacora.observaciones += " comentarios del usuario: " + model.comentarios;
                    bitacora.fecha = DateTime.Now;

                    bitacora.adjunto = model.adjuntos;

                    if (alerta.tipoAlerta == Constants.AlbaKeneth)
                    {
                        contenido = contenido.Replace("[noAlerta]", "Alba-Keneth No." + alerta.codigoAlerta);
                        contenido = contenido.Replace("[persona]", "del NNA");
                        contenido = contenido.Replace("[tipoAlerta]", "Alba-Keneth");
                        email.subjet = "[SICM] Notificación Alerta Alba-Keneth No." + alerta.codigoAlerta;
                        email.body = contenido;

                        await _emailService.sendActionEmailAK(email);
                    }
                    else
                    {
                        contenido = contenido.Replace("[noAlerta]", "Isabel-Claudina No." + alerta.codigoAlerta);
                        contenido = contenido.Replace("[persona]", "de la mujer");
                        contenido = contenido.Replace("[tipoAlerta]", "Isabel-Claudina");
                        email.subjet = "[SICM] Notificación Alerta Isabel-Claudina No." + alerta.codigoAlerta;
                        email.body = contenido;

                        await _emailService.sendActionEmailIC(email);
                    }

                    await _bitacoraRepo.Add(bitacora);

                    res.codigo = Constants.OK_code;
                    res.data = new { message = "Se cambio la situación del boletín" };
                }
                else
                {
                    res.codigo = Constants.NoContent_code;
                    res.data = "No se encontró el código de boletín";
                }
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Ultimas Acciones
        public async Task<Response> GetUltimasAcciones(UltimasAcciones_DTO model, PaginationFilter filter)
        {
            Response objR = new Response();
            try
            {
                List<UltimasAcciones> bitacoras = await _bitacoraRepo.getUltimasAcciones(model.tipoAlerta);
                bitacoras = bitacoras.Where(x => x.fechaAccion >= model.fechaInicio && x.fechaAccion <= model.fechaFin).ToList();
                int total = bitacoras.Count();

                bitacoras = bitacoras.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();


                objR.codigo = 200;
                objR.data = new { ultimasAcciones = bitacoras, total = total };
            }
            catch (Exception ex)
            {
                objR.codigo = 500;
                objR.message = ex.Message;
            }

            return objR;
        }
        #endregion
    }
}