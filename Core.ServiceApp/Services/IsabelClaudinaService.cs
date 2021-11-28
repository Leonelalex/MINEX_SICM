using AutoMapper;
using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.DTO.Core_DTOs.Responses;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.ViewModels;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.SICM_Repositories;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using External.ServiceApp.DTOs;
using External.ServiceApp.Services;
using External.ServiceApp.Services.ServicesContracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static External.ServiceApp.Services.MPService;

namespace Core.ServiceApp.Services
{
    public class IsabelClaudinaService : IIsabelClaudinaService
    {
        #region dependency injection
        private readonly IMapper _mapper;
        private readonly ISICM_ALERTAS_REPO _alertasRepo;
        private readonly ISICM_ALERTA_MISIONES_REPO _alertaMisionesRepo;
        private readonly ISICM_BITACORA_ALERTA_REPO _bitacoraRepo;
        private readonly IEmailService _emailService;


        public readonly ISICM_TIPO_NARIZ_REPO _tIPO_NARIZ_REPO;
        public readonly ISICM_TIPO_CEJA_REPO _tIPO_CEJA_REPO;
        public readonly ISICM_TIPO_CABELLO_REPO _tIPO_CABELLO_REPO;
        public readonly ISICM_TEZ_REPO _tEZ_REPO;
        public readonly ISICM_TAMANIO_CABELLO_REPO _tAMANIO_CABELLO_REPO;
        public readonly ISICM_COMPLEXION_REPO _cOMPLEXION_REPO;
        public readonly ISICM_COLOR_OJOS_REPO _cOLOR_OJOS_REPO;
        public readonly ISICM_COLOR_CABELLO_REPO _cOLOR_CABELLO_REPO;
        public readonly ISICM_BOLETINES_REPO _BOLETINES_REPO;

        private readonly ISICM_ACCIONES_ALERTA_REPO _accionesAlertaRepo;
        private readonly IMPService _iMPService;
        #endregion

        #region Constants private
        private const string pdf = ".pdf";
        private const string image = ".jpg";
        #endregion

        #region Builder
        public IsabelClaudinaService(ISICM_TIPO_NARIZ_REPO tIPO_NARIZ_REPO,
            ISICM_TIPO_CEJA_REPO tIPO_CEJA_REPO, ISICM_TIPO_CABELLO_REPO tIPO_CABELLO_REPO, ISICM_TEZ_REPO tEZ_REPO, ISICM_TAMANIO_CABELLO_REPO tAMANIO_CABELLO_REPO,
            ISICM_COMPLEXION_REPO cOMPLEXION_REPO, ISICM_COLOR_OJOS_REPO cOLOR_OJOS_REPO, ISICM_COLOR_CABELLO_REPO cOLOR_CABELLO_REPO,
            ISICM_ALERTAS_REPO alertasRepo, ISICM_ALERTA_MISIONES_REPO alertaMisionesRepo, ICatalogosService catalogosService,
            IMapper mapper, ISICM_BITACORA_ALERTA_REPO bitacoraRepo, IEmailService emailService, ISICM_ACCIONES_ALERTA_REPO accionesAlertaRepo, IMPService mPService,
            ISICM_BOLETINES_REPO boletinesRepo)
        {
            _mapper = mapper;
            _alertasRepo = alertasRepo;
            _alertaMisionesRepo = alertaMisionesRepo;
            _bitacoraRepo = bitacoraRepo;
            _emailService = emailService;

            _tIPO_NARIZ_REPO = tIPO_NARIZ_REPO;
            _tIPO_CEJA_REPO = tIPO_CEJA_REPO;
            _tIPO_CABELLO_REPO = tIPO_CABELLO_REPO;
            _tEZ_REPO = tEZ_REPO;
            _tAMANIO_CABELLO_REPO = tAMANIO_CABELLO_REPO;
            _cOMPLEXION_REPO = cOMPLEXION_REPO;
            _cOLOR_OJOS_REPO = cOLOR_OJOS_REPO;
            _cOLOR_CABELLO_REPO = cOLOR_CABELLO_REPO;
            _BOLETINES_REPO = boletinesRepo;

            _accionesAlertaRepo = accionesAlertaRepo;
            _iMPService = mPService;

        }
        #endregion

        #region Get Alertas
        #region Get Alertas paginadas
        public async Task<Response> GetAll(PaginationFilter filter)
        {
            Response res = new Response();
            try
            {
                var test = await _alertasRepo.getAlertasIsabelClaudina(filter);

                var alertasObj = _mapper.Map<IEnumerable<AlertasAlbaKeneth_ViewModel>>(await _alertasRepo.getAlertasIsabelClaudina(filter));
                var totalAlertas = _alertasRepo.countAlertasIsabelClaudina();

                res.codigo = 200;
                res.data = new { alertas = alertasObj, total = totalAlertas };
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al obtener Alertas Isabel-Claudina";
            }

            return res;
        }
        #endregion

        #region Get Bitacora Alertas
        public async Task<Response> GetBitacora(int codAlerta)
        {
            Response res = new Response();

            try
            {
                SICM_ALERTAS alerta = _alertasRepo.GetByID(codAlerta).Result;
                if (alerta.tipoAlerta == 2)
                {
                    var bitacora = _mapper.Map<IEnumerable<bitacoraViewModel>>(await _bitacoraRepo.getByCodigoAlerta(codAlerta));
                    res.codigo = 200;
                    res.data = bitacora;
                }
                else
                {
                    res.codigo = 204;
                    res.data = new { codigo = 204, message = "Alerta no encontrada" };
                }

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al obtener bitacora de alerta";

            }
            return res;
        }
        #endregion

        #region Get Alertas sin activar
        public async Task<Response> GetAlertasSinActivar(PaginationFilter filter)
        {
            Response res = new Response();
            try
            {
                IEnumerable<AlertasAlbaKeneth_ViewModel> alertas = _mapper.Map<IEnumerable<AlertasAlbaKeneth_ViewModel>>(await _alertasRepo.getAlertaICSinActivar());
                int total = alertas.Count();
                alertas = alertas.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

                if (alertas != null)
                {
                    res.codigo = 200;
                    res.data = new { alertas = alertas, total = total };
                }
                else
                {
                    res.codigo = 203;
                    res.data = new { message = "No se han enontrado alertas" };
                }

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = ex.Message;
            }
            return res;
        }
        #endregion

        #region difución de alerta Isabel-Claudina
        public async Task<Response> diffusionIC(DiffusionIC_DTO model, int codAlerta)
        {
            Response objR = new Response();
            try
            {
                await _alertaMisionesRepo.DeleteByCodigoAlerta(codAlerta);

                SICM_ALERTAS alerta = _alertasRepo.GetByID(codAlerta).Result;

                //agregar la validacion de difusion internacional

                if (model.misiones.Count != 0)
                {
                    foreach (int itemmision in model.misiones)
                    {
                        SICM_ALERTA_MISIONES alertaMision = new SICM_ALERTA_MISIONES();

                        alertaMision.CodigoMision = itemmision;
                        alertaMision.CodigoAlerta = codAlerta;
                        await _alertaMisionesRepo.Add(alertaMision);
                    }
                }

                objR.codigo = 200;
                objR.data = new { codigo = 200, message = "Se agregaron misiones exitosamente" };

            }
            catch (Exception ex)
            {
                objR.codigo = 500;
                objR.message = ex.Message;
            }
            return objR;
        }
        #endregion
        #endregion

        #region CRUD alertas Isabel-Claudina

        #region Crear Alerta
        public async Task<AlertaIsabelClaudina_ViewModel> CreateAlert(AlertaIsabelClaudina_DTO model)
        {
            AlertaIsabelClaudina_ViewModel res = new AlertaIsabelClaudina_ViewModel();
            res.respuesta = new RespuestaMP_ViewModel { institucionOrigen = Constants.InstitutionMinex, recepcionRespuesta = true };
            res.responseTime = DateTime.Now;

            EmailModel email = new EmailModel();

            try
            {
                if (model.alerta.flag_activacion_internacional.Equals(false))
                {
                    res.respuesta.recepcionRespuesta = false;
                    res.idError = Constants.Error_code;
                    res.message = "Alerta no registrada, no es para publicación internacional";
                    return res;
                }
                data archivConvert = new data();
                SICM_ALERTAS newAlerta = new SICM_ALERTAS();
                newAlerta.codigoAlerta = model.alerta.numero_alerta;
                newAlerta.direccion = model.alerta.direccion.numero_casa + " " +
                    model.alerta.direccion.colonia + " " +
                    model.alerta.direccion.numero_calle + "-" +
                    model.alerta.direccion.numero_avenida + " ";
                newAlerta.estadoAlerta = Constants.CodeRegisterAlert;
                newAlerta.tipoAlerta = Constants.IsabelClaudina;
                newAlerta.fechaActivacion = DateTime.Now;
                newAlerta.correosAdicionales = null;

                if (!String.IsNullOrEmpty(model.alerta.id_cloud_oficio_internacional_firmado))
                {
                    archivConvert = await _iMPService.getFileMP(model.alerta.id_cloud_oficio_internacional_firmado);

                    if (archivConvert.valid == true)
                    {
                        if (!String.IsNullOrEmpty(archivConvert.archivo))
                        {
                            archivConvert.idFile = model.alerta.id_cloud_oficio_internacional_firmado;
                            await Savearchivo(archivConvert, pdf);
                            newAlerta.oficio = model.alerta.id_cloud_oficio_internacional_firmado + pdf;//se guarda el oficio internacional
                        }
                    }
                    else
                    {
                        res.respuesta.recepcionRespuesta = false;
                        res.idError = Constants.Error_code;
                        res.message = "No se pudo almacenar el oficio " + model.alerta.id_cloud_oficio_internacional_firmado;
                        res.ErrorMessage = archivConvert.message;

                        #region envio de correo si falla la obtención de boletín
                        SICM_ACCIONES_ALERTA accion1 = new SICM_ACCIONES_ALERTA();
                        accion1 = await _accionesAlertaRepo.GetByID(17);

                        string contenidoEmail1 = accion1.ContenidoEmail;
                        contenidoEmail1 = contenidoEmail1.Replace("[noOficio]", "123456789");
                        contenidoEmail1 = contenidoEmail1.Replace("[noAlerta]", model.alerta.numero_alerta);
                        contenidoEmail1 = contenidoEmail1.Replace("[noArchivo]", model.alerta.id_cloud_oficio_internacional_firmado);

                        email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                        email.oficio = model.alerta.id_cloud_oficio_internacional_firmado + pdf;
                        email.subjet = "[SICM] Alerta Isabel-Claudina no registrada";
                        email.body = contenidoEmail1;
                        email.titulo = "Error al registrar Alerta Isabel-Claudina No." + model.alerta.numero_alerta;
                        email.saludo = "Señores funcionarios del Ministerio Público:";

                        await _emailService.sendEmaiErrorlIC(email);
                        #endregion

                        return res;
                    }
                }
                else
                {
                    res.respuesta.recepcionRespuesta = false;
                    res.idError = Constants.Error_code;
                    res.message = "Alerta sin oficio " + model.alerta.id_cloud_oficio_internacional_firmado;
                    res.ErrorMessage = archivConvert.message;

                    #region envio de correo si falla la obtención de boletín
                    SICM_ACCIONES_ALERTA accion1 = new SICM_ACCIONES_ALERTA();
                    accion1 = await _accionesAlertaRepo.GetByID(17);

                    string contenidoEmail1 = accion1.ContenidoEmail;
                    contenidoEmail1 = contenidoEmail1.Replace("[noOficio]", "123456789");
                    contenidoEmail1 = contenidoEmail1.Replace("[noAlerta]", model.alerta.numero_alerta);
                    contenidoEmail1 = contenidoEmail1.Replace("[noArchivo]", model.alerta.id_cloud_oficio_internacional_firmado);

                    email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                                                                                       // email.oficio = model.alerta.id_cloud_oficio_internacional_firmado + pdf;
                    email.subjet = "[SICM] Alerta Isabel-Claudina no registrada";
                    email.body = contenidoEmail1;
                    email.titulo = "Error al registrar Alerta Isabel-Claudina No." + model.alerta.numero_alerta;
                    email.saludo = "Señores funcionarios del Ministerio Público:";

                    await _emailService.sendEmaiErrorlIC(email);
                    #endregion

                    return res;
                }

                SICM_BOLETINES boletin = new SICM_BOLETINES();
                boletin.primerNombre = model.alerta.primerNombre.ToUpper();
                boletin.segundoNombre = model.alerta.segundoNombre.ToUpper();
                boletin.tercerNombre = model.alerta.otroNombre.ToUpper();
                boletin.primerApellido = model.alerta.primerApellido.ToUpper();
                boletin.segundoApellido = model.alerta.segundoApellido.ToUpper();
                boletin.apellidoCasada = model.alerta.apellidoCasada.ToUpper();

                boletin.estatura = Convert.ToInt32(model.alerta.estatura * 100);
                boletin.vestimenta = model.alerta.vestimenta;
                boletin.genero = Constants.CodeFemale;
                boletin.codigoSituacion = 9;//sin situacion especial
                boletin.cui = model.alerta.cui;
                boletin.codigoEstatus = 1;
                boletin.notas = model.alerta.observacion;
                boletin.edad = model.alerta.edad;

                if (!String.IsNullOrEmpty(model.alerta.id_cloud_boletin))
                {
                    archivConvert = await _iMPService.getFileMP(model.alerta.id_cloud_boletin);

                    if (archivConvert.valid == true)
                    {
                        if (!String.IsNullOrEmpty(archivConvert.archivo))
                        {
                            archivConvert.idFile = model.alerta.id_cloud_boletin;
                            await Savearchivo(archivConvert, image);
                            boletin.boletin = model.alerta.id_cloud_boletin + image;
                        }
                    }
                    else
                    {
                        res.respuesta.recepcionRespuesta = false;
                        res.idError = Constants.Error_code;
                        res.message = "No se pudo almacenar el boletín " + model.alerta.id_cloud_boletin;
                        res.ErrorMessage = archivConvert.message;

                        #region envio de correo si falla la obtención de boletín
                        SICM_ACCIONES_ALERTA accion1 = new SICM_ACCIONES_ALERTA();
                        accion1 = await _accionesAlertaRepo.GetByID(17);

                        string contenidoEmail1 = accion1.ContenidoEmail;
                        contenidoEmail1 = contenidoEmail1.Replace("[noOficio]", "123456789");
                        contenidoEmail1 = contenidoEmail1.Replace("[noAlerta]", model.alerta.numero_alerta);
                        contenidoEmail1 = contenidoEmail1.Replace("[noArchivo]", model.alerta.id_cloud_boletin);

                        email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                        email.oficio = model.alerta.id_cloud_oficio_internacional_firmado + pdf;
                        email.subjet = "[SICM] Alerta Isabel-Claudina no registrada";
                        email.body = contenidoEmail1;
                        email.titulo = "Error al registrar Alerta Isabel-Claudina No." + model.alerta.numero_alerta;
                        email.saludo = "Señores funcionarios del Ministerio Público:";

                        await _emailService.sendEmaiErrorlIC(email);
                        #endregion

                        return res;
                    }
                }
                else
                {
                    res.respuesta.recepcionRespuesta = false;
                    res.idError = Constants.Error_code;
                    res.message = "Alerta sin boletin " + model.alerta.id_cloud_boletin;
                    res.ErrorMessage = archivConvert.message;

                    #region envio de correo si falla la obtención de boletín
                    SICM_ACCIONES_ALERTA accion1 = new SICM_ACCIONES_ALERTA();
                    accion1 = await _accionesAlertaRepo.GetByID(17);

                    string contenidoEmail1 = accion1.ContenidoEmail;
                    contenidoEmail1 = contenidoEmail1.Replace("[noOficio]", "123456789");
                    contenidoEmail1 = contenidoEmail1.Replace("[noAlerta]", model.alerta.numero_alerta);
                    contenidoEmail1 = contenidoEmail1.Replace("[noArchivo]", model.alerta.id_cloud_boletin);

                    email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                    email.oficio = model.alerta.id_cloud_oficio_internacional_firmado + pdf;
                    email.subjet = "[SICM] Alerta Isabel-Claudina no registrada";
                    email.body = contenidoEmail1;
                    email.titulo = "Error al registrar Alerta Isabel-Claudina No." + model.alerta.numero_alerta;
                    email.saludo = "Señores funcionarios del Ministerio Público:";

                    await _emailService.sendEmaiErrorlIC(email);
                    #endregion

                    return res;
                }

                if (await _cOLOR_CABELLO_REPO.ExistAsync(model.alerta.id_color_cabello))
                    boletin.colorCabello = model.alerta.id_color_cabello;

                if (await _tIPO_CABELLO_REPO.ExistAsync(model.alerta.id_tipo_cabello))
                    boletin.tipoCabello = model.alerta.id_tipo_cabello;

                if (await _tAMANIO_CABELLO_REPO.ExistAsync(model.alerta.id_longitud_cabello))
                    boletin.tamanioCabello = model.alerta.id_longitud_cabello;

                if (await _tIPO_CEJA_REPO.ExistAsync(model.alerta.id_tipo_ceja))
                    boletin.tipoCeja = model.alerta.id_tipo_ceja;

                if (await _cOLOR_OJOS_REPO.ExistAsync(model.alerta.id_tipo_ojo))
                    boletin.colorOjos = model.alerta.id_tipo_ojo;

                if (await _tIPO_NARIZ_REPO.ExistAsync(model.alerta.id_tipo_nariz))
                    boletin.tipoNariz = model.alerta.id_tipo_nariz;

                if (await _cOMPLEXION_REPO.ExistAsync(model.alerta.id_complexion))
                    boletin.complexion = model.alerta.id_complexion;

                if (await _tEZ_REPO.ExistAsync(model.alerta.id_tipo_color_piel))
                    boletin.tez = model.alerta.id_tipo_color_piel;

                SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();
                bitacora.usuario = model.usuario;
                bitacora.pais = model.institucion;
                bitacora.codigoEstado = Constants.CodeRegisterAlert;
                bitacora.codigoAccion = 10;
                bitacora.observaciones = model.alerta.observacion;
                bitacora.adjunto = model.alerta.id_cloud_oficio_internacional_firmado + pdf;
                bitacora.fecha = DateTime.Now;

                newAlerta.SicmBitacoraAlerta.Add(bitacora);
                newAlerta.SicmBoletines.Add(boletin);
                await _alertasRepo.Add(newAlerta);

                SICM_ACCIONES_ALERTA accion = new SICM_ACCIONES_ALERTA();
                accion = await _accionesAlertaRepo.GetByID(10);

                string contenidoEmail = accion.ContenidoEmail;
                contenidoEmail = contenidoEmail.Replace("[noOficio]", "123456789");
                contenidoEmail = contenidoEmail.Replace("[noAlerta]", newAlerta.codigoAlerta);

                email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                email.oficio = model.alerta.id_cloud_oficio_internacional_firmado + pdf;
                email.subjet = "[SICM] Alerta Isabel-Claudina Registrada";
                email.body = contenidoEmail;
                email.boletinIC = model.alerta.id_cloud_boletin + image;
                email.titulo = "Registro de Alerta Isabel-Claudina No." + newAlerta.codigoAlerta;
                email.saludo = "Señores funcionarios del Ministerio Público:";
                email.datosAlerta = "";

                if (!await _emailService.sendEmailIC(email))
                {
                    await _bitacoraRepo.Delete(bitacora.Codigo);
                    await _BOLETINES_REPO.Delete(boletin.codigoBoletin);
                    await _alertasRepo.Delete(newAlerta.codigo);

                    res.idError = Constants.Error_code;
                    res.message = "Error la enviar correo";
                    return res;
                }

                res.idError = Constants.OK_code;
                res.message = Constants.CreateAlertOk;

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    res.respuesta.recepcionRespuesta = false;
                    res.idError = Constants.Error_code;
                    res.message = "El número de alerta ya existe";
                    res.ErrorMessage = ex.InnerException.Message;

                    //traemos las acciones
                    SICM_ACCIONES_ALERTA accion = new SICM_ACCIONES_ALERTA();
                    accion = await _accionesAlertaRepo.GetByID(17);

                    string contenidoEmail = accion.ContenidoEmail;
                    contenidoEmail = contenidoEmail.Replace("[noOficio]", "123456789");
                    contenidoEmail = contenidoEmail.Replace("[noAlerta]", model.alerta.numero_alerta);
                    // contenidoEmail + "con los archivos " + model.alerta.id_cloud_oficio_internacional_firmado + ", " + model.alerta.id_cloud_boletin;

                    email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                    email.subjet = "[SICM] Alerta Isabel-Claudina Activada";
                    email.body = contenidoEmail;
                    email.titulo = "Error al registrar Alerta Repetira Isabel-Claudina No." + model.alerta.numero_alerta;
                    email.saludo = "Señores funcionarios del Ministerio Público:";

                    await _emailService.sendEmaiErrorlIC(email);
                }
                else
                {
                    res.respuesta.recepcionRespuesta = false;
                    res.idError = Constants.Error_code;
                    res.message = "Hubo un error al crear alerta o ya existe";
                    res.ErrorMessage = ex.InnerException.Message;

                    //traemos las acciones
                    SICM_ACCIONES_ALERTA accion = new SICM_ACCIONES_ALERTA();
                    accion = await _accionesAlertaRepo.GetByID(17);

                    string contenidoEmail = accion.ContenidoEmail;
                    contenidoEmail = contenidoEmail.Replace("[noOficio]", "123456789");
                    contenidoEmail = contenidoEmail.Replace("[noAlerta]", model.alerta.numero_alerta);
                    // contenidoEmail + "con los archivos " + model.alerta.id_cloud_oficio_internacional_firmado + ", " + model.alerta.id_cloud_boletin;

                    email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                    email.subjet = "[SICM] Alerta Isabel-Claudina Error";
                    email.body = contenidoEmail;
                    email.titulo = "Error al registrar Alerta Repetira Isabel-Claudina No." + model.alerta.numero_alerta;
                    email.saludo = "Señores funcionarios del Ministerio Público:";

                    await _emailService.sendEmaiErrorlIC(email);
                }

            }
            catch (Exception ex)
            {
                res.respuesta.recepcionRespuesta = false;

                res.idError = Constants.Error_code;
                res.message = Constants.NoCreateAlert;
                res.ErrorMessage = ex.InnerException.Message;

                //traemos las acciones
                SICM_ACCIONES_ALERTA accion = new SICM_ACCIONES_ALERTA();
                accion = await _accionesAlertaRepo.GetByID(17);

                string contenidoEmail = accion.ContenidoEmail;
                contenidoEmail = contenidoEmail.Replace("[noOficio]", "123456789");
                contenidoEmail = contenidoEmail.Replace("[noAlerta]", model.alerta.numero_alerta);
                // contenidoEmail + "con los archivos " + model.alerta.id_cloud_oficio_internacional_firmado + ", " + model.alerta.id_cloud_boletin;

                email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt";//,jeleon@minex.gob.gt,asierra@minex.gob.gt,vestrada@minex.gob.gt";
                email.subjet = "[SICM] Alerta Isabel-Claudina Activada";
                email.body = contenidoEmail;
                email.titulo = "Error al registrar Alerta Isabel-Claudina No." + model.alerta.numero_alerta;
                email.saludo = "Señores funcionarios del Ministerio Público:";

                await _emailService.sendEmaiErrorlIC(email);
            }

            return res;

        }
        #endregion

        #region Activar Alerta
        public async Task<Response> ActivateAlert(Activar_IC_DTO misiones, int codAlerta)
        {
            Response res = new Response();
            try
            {
                SICM_ALERTAS alerta = await _alertasRepo.GetByID(codAlerta);

                if (alerta != null)
                {
                    alerta.estadoAlerta = 2;
                    alerta.correosAdicionales = misiones.correosAdicionales;
                    alerta.difusionInternacional = misiones.difusionInternacional;

                    

                    SICM_ACCIONES_ALERTA accion = await _accionesAlertaRepo.GetByID(10);

                    if (!misiones.difusionInternacional)
                    {
                        foreach (int itemmision in misiones.misiones)
                        {
                            SICM_ALERTA_MISIONES alertaMision = new SICM_ALERTA_MISIONES
                            {

                                CodigoMision = itemmision,
                                CodigoAlerta = codAlerta
                            };

                            await _alertaMisionesRepo.Add(alertaMision);
                        }
                    }

                    EmailModel email = new EmailModel();

                    string contenidoEmail = accion.ContenidoEmail;

                    contenidoEmail = contenidoEmail.Replace("[noOficio]", "oficio");//alerta.codigoOficio);
                    contenidoEmail = contenidoEmail.Replace("[noAlerta]", alerta.codigoAlerta);
                    contenidoEmail = contenidoEmail.Replace("[observaciones]", alerta.observaciones);

                    email.listCorreos = misiones.correosAdicionales;
                    email.oficio = alerta.oficio; //aca ira el nombre del oficio para ir a leerlo
                    email.subjet = "[SICM] Alerta Isabel-Claudina Activada";
                    email.body = contenidoEmail;
                    string nameboletin = _BOLETINES_REPO.getBoletin(codAlerta);
                    email.boletinIC = nameboletin; //aca ira el nombre del boletin
                    email.titulo = "Difusión de Alerta Isabel-Claudina No." + alerta.codigoAlerta;
                    email.saludo = "Señores funcionarios:";
                    //email.datosAlerta = "";
                    if (!await _emailService.sendEmailIC(email))
                    {
                        res.codigo = Constants.Error_code;
                        res.data = new { codigo = Constants.Error_code, message = "No se pudo difundir alerta" };
                     
                    }

                    SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();
                    bitacora.usuario = GlobalsVariables.UserName;
                    bitacora.pais = GlobalsVariables.Pais;
                    bitacora.codigoEstado = Constants.CodeRegisterAlert;
                    bitacora.codigoAccion = 10;
                    bitacora.observaciones = "Alerta difundida internacionalmente";
                    bitacora.fecha = DateTime.Now;

                    alerta.SicmBitacoraAlerta.Add(bitacora);
                    await _alertasRepo.Update(alerta);
                 
                    res.codigo = Constants.OK_code;
                    res.data = new { codigo = Constants.OK_code, message = "Difusión con éxito" };
                }

                res.codigo = Constants.NoContent_code;
                res.data = new { message = "No se encontro el codigo de la alerta" };

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Desactivar Alerta
        public async Task<AlertaIsabelClaudina_ViewModel> DesactiveAlert(AlertaIsaChangeStatusDTO model)
        {
            AlertaIsabelClaudina_ViewModel res = new AlertaIsabelClaudina_ViewModel();
            res.respuesta = new RespuestaMP_ViewModel { institucionOrigen = Constants.InstitutionMinex, recepcionRespuesta = true };
            res.responseTime = DateTime.Now;

            EmailModel email = new EmailModel();

            try
            {
                data archivConvert = new data();

                SICM_ALERTAS modelnew = await _alertasRepo.getOjAllByCodeAlert(model.alerta.numero_alerta);

                SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();
                bitacora.usuario = model.usuario;
                bitacora.pais = model.institucion;
                bitacora.codigoAlerta = modelnew.codigo;
                bitacora.codigoEstado = Constants.CodeDesactiveAlert;

                bitacora.codigoAccion = 16;
                bitacora.observaciones = "Desactivación de alerta Isabel-Claudina";

                if (!String.IsNullOrEmpty(model.alerta.id_cloud_oficio_desactivacion_firmado))
                {
                    archivConvert = await _iMPService.getFileMP(model.alerta.id_cloud_oficio_desactivacion_firmado);

                    if (archivConvert.valid == true)
                    {
                        if (!String.IsNullOrEmpty(archivConvert.archivo))
                        {
                            archivConvert.idFile = model.alerta.id_cloud_oficio_desactivacion_firmado;
                            await Savearchivo(archivConvert, pdf);
                            bitacora.adjunto = model.alerta.id_cloud_oficio_desactivacion_firmado + pdf;//se guarda el oficio internacional
                        }
                    }
                    else
                    {
                        res.respuesta.recepcionRespuesta = false;
                        res.idError = Constants.Error_code;
                        res.message = "No se pudo almacenar el oficio " + model.alerta.id_cloud_oficio_desactivacion_firmado;
                        res.ErrorMessage = archivConvert.message;
                        return res;
                    }
                }

                bitacora.fecha = DateTime.Now;

                modelnew.estadoAlerta = Constants.CodeDesactiveAlert;
                modelnew.fechaDesactivacion = DateTime.Now;
                modelnew.SicmBitacoraAlerta.Add(bitacora);

                await _alertasRepo.Update(modelnew);

                SICM_ACCIONES_ALERTA accion = new SICM_ACCIONES_ALERTA();
                accion = await _accionesAlertaRepo.GetByID(16);

                string contenidoEmail = accion.ContenidoEmail;
                contenidoEmail = contenidoEmail.Replace("[noOficio]", model.alerta.id_cloud_oficio_desactivacion_firmado);
                contenidoEmail = contenidoEmail.Replace("[noAlerta]", model.alerta.numero_alerta);

                email.listCorreos = "lframirez@minex.gob.gt,lrecinos@minex.gob.gt,morales.gestiones@gmail.com";
                email.oficio = model.alerta.id_cloud_oficio_desactivacion_firmado + pdf; //aca ira el nombre del oficio para ir a leerlo
                email.subjet = "[SICM] Alerta Isabel-Claudina Activada";
                email.body = contenidoEmail;
                email.titulo = "Difusión de Alerta Isabel-Claudina No." + model.alerta.numero_alerta;
                email.saludo = "Señores funcionarios del Ministerio Público:";
                email.datosAlerta = "";

                await _emailService.sendEmailIC(email);

                res.idError = 200;
                res.message = Constants.UpdateAlertOk;

            }
            catch (Exception ex)
            {
                res.respuesta.recepcionRespuesta = false;
                res.idError = 500;
                res.message = Constants.NoUpdateAlert;
                res.ErrorMessage = ex.Message;
            }
            return res;
        }
        #endregion

        #region Get Alertas By Filters
        public async Task<Response> GetAlertsByFilters(AlertSearchFilters filters, PaginationFilter pagefilter)
        {
            Response res = new Response();
            try
            {
                IEnumerable<SICM_ALERTAS> alertas = await _alertasRepo.getAllIsabelClaudina();

                if (filters.codigoAlerta != null && filters.codigoAlerta != "")
                    alertas = alertas.Where(x => x.codigoAlerta == filters.codigoAlerta);

                if (filters.numeroCaso != null && filters.numeroCaso != "")
                    alertas = alertas.Where(x => x.numeroCaso == filters.numeroCaso);

                if (filters.codigoOficio != null && filters.codigoOficio != "")
                    alertas = alertas.Where(x => x.codigoOficio == filters.codigoOficio);

                if (filters.estado != 0)
                    alertas = alertas.Where(x => x.estadoAlerta == filters.estado);

                if (filters.fechaActivacionIni > DateTime.MinValue && filters.fechaActivacionFin > DateTime.MinValue)
                    alertas = alertas.Where(
                        x => x.fechaActivacion >= filters.fechaActivacionIni &&
                        x.fechaActivacion <= filters.fechaActivacionFin);

                if (filters.fechaActivacionIni > DateTime.MinValue && filters.fechaActivacionFin == DateTime.MinValue)
                    alertas = alertas.Where(x => x.fechaActivacion >= filters.fechaActivacionIni);

                if (filters.fechaDesactivacionIni > DateTime.MinValue && filters.fechaDesactivacionFin > DateTime.MinValue)
                    alertas = alertas.Where(
                        x => x.fechaDesactivacion >= filters.fechaDesactivacionIni &&
                        x.fechaDesactivacion <= filters.fechaDesactivacionFin);

                if (filters.fechaDesactivacionIni > DateTime.MinValue && filters.fechaDesactivacionFin == DateTime.MinValue)
                    alertas = alertas.Where(x => x.fechaDesactivacion >= filters.fechaDesactivacionIni);

                int total = alertas.Count();

                alertas = alertas.Skip((pagefilter.PageNumber - 1) * pagefilter.PageSize).Take(pagefilter.PageSize);

                IEnumerable<AlertasAlbaKeneth_ViewModel> resAlestas = _mapper.Map<IEnumerable<AlertasAlbaKeneth_ViewModel>>(alertas);

                res.codigo = 200;
                res.data = new { alertas = resAlestas, total = total };

            }
            catch (Exception ex)
            {
                res.codigo = 500;
                res.message = ex.Message;
            }
            return res;
        }
        #endregion

        #region guardarArchivosLocalmente
        public static async Task<bool> Savearchivo(data archivo, string type)
        {
            byte[] imgByte = Convert.FromBase64String(archivo.archivo);
            string path = "";

            try
            {
                if (type == pdf)
                    path = GlobalsVariables.ICOficios_Path;
                if (type == image)
                    path = GlobalsVariables.ICPics_Path;

                string fileName = archivo.idFile + type;

                string pahtboletin = Path.Combine(path, fileName);
                await File.WriteAllBytesAsync(pahtboletin, imgByte);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

    }
}
