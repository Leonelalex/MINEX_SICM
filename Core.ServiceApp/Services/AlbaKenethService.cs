using AutoMapper;
using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.DTO.Core_DTOs.Responses;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.ViewModels;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.Helpers.ViewModels;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using External.ServiceApp.DTOs;
using External.ServiceApp.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services
{
    public class AlbaKenethService: IAlbaKenethService
    {
        private readonly ICatalogosService _catalogosService;
        private readonly IMapper _mapper;
        private readonly ISICM_ALERTAS_REPO _alertasRepo;
        private readonly ISICM_BITACORA_ALERTA_REPO _bitacoraRepo;
        private readonly ISICM_ALERTA_MISIONES_REPO _alertaMisionesRepo;
        private readonly ISICM_BOLETINES_REPO _boletinesRepo;
        private readonly IGLOB_MISIONES_EXTERIOR_REPO _globMisionesExterior;
        private readonly IEmailService _emailService;
        private readonly ISICM_ACCIONES_ALERTA_REPO _accionesAlertaRepo;

        public AlbaKenethService(ICatalogosService catalogosService, IMapper mapper, ISICM_ALERTAS_REPO alertasRepo, ISICM_BITACORA_ALERTA_REPO bitacoraRepo,
            ISICM_ALERTA_MISIONES_REPO alertaMisionesRepo, ISICM_BOLETINES_REPO boletinesRepo, IGLOB_MISIONES_EXTERIOR_REPO misionesexterior,
            IEmailService emailService, ISICM_ACCIONES_ALERTA_REPO accionesAlertaRepo)
        {
            _mapper = mapper;
            _catalogosService = catalogosService;
            _alertasRepo = alertasRepo;
            _bitacoraRepo = bitacoraRepo;
            _alertaMisionesRepo = alertaMisionesRepo;
            _boletinesRepo = boletinesRepo;
            _globMisionesExterior = misionesexterior;
            _emailService = emailService;
            _accionesAlertaRepo = accionesAlertaRepo;
        }

        #region CRUD alertas

        #region Crear Alerta
        public async Task<Response> CrearAlerta(AlertaAlbaKeneth_DTO model)
        {
            Response res = new Response();

            try
            {
                SICM_ACCIONES_ALERTA accion = new SICM_ACCIONES_ALERTA();
                accion = await _accionesAlertaRepo.GetByID(1);

                SICM_ALERTAS alertaModel = _mapper.Map<SICM_ALERTAS>(model);

                SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                bitacora.usuario = GlobalsVariables.UserName;
                bitacora.pais = GlobalsVariables.Pais;
                bitacora.codigoEstado = 2;
                bitacora.codigoAccion = accion.Codigo;
                bitacora.observaciones = alertaModel.observaciones;
                bitacora.adjunto = alertaModel.oficio;
                bitacora.fecha = DateTime.Now;
                bitacora.CodigoAlertaNavigation = alertaModel;

                alertaModel.SicmBitacoraAlerta.Add(bitacora);
                alertaModel.estadoAlerta = 2;
                alertaModel.tipoAlerta = 1;

                alertaModel.fechaActivacion = DateTime.Now;
                string docs = "";

                if (model.boletines.Count > 0)
                {
                    foreach (var boletin in model.boletines)
                    {
                        SICM_BOLETINES b = _mapper.Map<SICM_BOLETINES>(boletin);

                        b.codigoEstatus = 1;
                        b.codigoSituacion = 9;
                        b.CodigoAlertaNavigation = alertaModel;
                        b.tipoCeja = 999;
                        b.tipoNariz = 999;

                        docs += b.boletin + ",";

                        alertaModel.SicmBoletines.Add(b);

                    }

                    docs = docs.Remove(docs.Length - 1, 1);
                }

                foreach (int mision in model.misiones)
                {
                    SICM_ALERTA_MISIONES alertaMision = new SICM_ALERTA_MISIONES();

                    alertaMision.CodigoMision = mision;

                    alertaMision.CodigoAlertaNavigation = alertaModel;

                    alertaModel.SicmAlertaMisiones.Add(alertaMision);
                }

                await _alertasRepo.Add(alertaModel);

                EmailModel email = new EmailModel();

                string contenidoEmail = accion.ContenidoEmail;

                contenidoEmail = contenidoEmail.Replace("[noOficio]", alertaModel.codigoOficio);
                contenidoEmail = contenidoEmail.Replace("[noAlerta]", alertaModel.codigoAlerta);
                contenidoEmail = contenidoEmail.Replace("[observaciones]", alertaModel.observaciones);

                email.listCorreos = model.correosAdicionales;
                email.oficio = model.oficio;
                email.listDocs = docs;
                email.subjet = "[SICM] Alerta Alba-Keneth Activada";
                email.body = contenidoEmail;
                email.titulo = "Difusión de Alerta Alba-Keneth No." + alertaModel.codigoAlerta;
                email.saludo = "Señores funcionarios:";
                email.datosAlerta = "";
                

                await _emailService.sendEmailAK(email);

                res.codigo = Constants.OK_code;
                res.data = new { codigo = Constants.OK_code, message = "Alerta creada exitosamente" };

            }catch(DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    res.codigo = Constants.Error_code;
                    res.innerError = ex.InnerException.Message;
                    res.message = "El número de alerta ya existe";
                }
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "El número de alerta ya existe";
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al intentar crear la alerta";
            }
            return res;

        }
        #endregion

        #region Get Alertas
        public async Task<Response> GetAllAlerts(PaginationFilter filter)
        {
            Response res = new Response();
            try
            {
                var alertasObj = _mapper.Map<IEnumerable<AlertasAlbaKeneth_ViewModel>>(await _alertasRepo.getAlertasAlbaKeneth(filter));
                var totalAlertas = _alertasRepo.countAlertasAlbaKeneth();


                res.codigo = Constants.OK_code;
                res.data = new { alertas = alertasObj, total = totalAlertas };
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al intentar obtener las alertas";
            }

            return res;
        }
        #endregion

        #region Update Alerta
        public async Task<Response> UpdateAlerta(AlertaAlbaKeneth_DTO model, int codAlerta)
        {
            Response res = new Response();
            try
            {

                SICM_ALERTAS alertaModel = await _alertasRepo.GetByID(codAlerta);

                if (!String.IsNullOrEmpty(model.codigoAlerta))
                    alertaModel.codigoAlerta = model.codigoAlerta;
                if (!String.IsNullOrEmpty(model.direccion))
                    alertaModel.direccion = model.direccion;
                if (model.codigoMunicipio != 0)
                    alertaModel.codigoMunicipio = model.codigoMunicipio;
                if (!String.IsNullOrEmpty(model.codigoOficio))
                    alertaModel.codigoOficio = model.codigoOficio;
                if (!String.IsNullOrEmpty(model.oficio))
                    alertaModel.oficio = model.oficio;
                if (!String.IsNullOrEmpty(model.observaciones))
                    alertaModel.observaciones = model.observaciones;
                if (!String.IsNullOrEmpty(model.correosAdicionales))
                    alertaModel.correosAdicionales = model.correosAdicionales;
                if (!String.IsNullOrEmpty(model.numeroCaso))
                    alertaModel.numeroCaso = model.numeroCaso;

                await _alertasRepo.Update(alertaModel);

                if (model.misiones != null)
                {
                    await _alertaMisionesRepo.DeleteByCodigoAlerta(codAlerta);

                    foreach (int mision in model.misiones)
                    {
                        SICM_ALERTA_MISIONES alertaMision = new SICM_ALERTA_MISIONES();

                        alertaMision.CodigoMision = mision;
                        alertaMision.CodigoAlerta = codAlerta;

                        await _alertaMisionesRepo.Add(alertaMision);
                    }
                }

                SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                bitacora.usuario = GlobalsVariables.UserName;
                bitacora.pais = GlobalsVariables.Pais;
                bitacora.codigoEstado = alertaModel.estadoAlerta;
                bitacora.codigoAccion = 5;
                bitacora.observaciones = "Se edito la información de la alerta";
                bitacora.fecha = DateTime.Now;
                bitacora.codigoAlerta = alertaModel.codigo;

                await _bitacoraRepo.Add(bitacora);

                res.codigo = 200;
                res.data = new { codigo = 200, message = "Alerta modificada con éxito" };

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    res.codigo = Constants.Error_code;
                    res.message = "El número de alerta ya existe";
                }
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al intentar editar la alerta";
            }
            return res;
        }
        #endregion

        #region Desactivar Alerta
        public async Task<Response> DesactivarAlerta(DesactivarAlerta_AK_DTO model, int codAlerta)
        {
            Response res = new Response();
            try
            {

                SICM_ALERTAS alerta = await _alertasRepo.GetByID(codAlerta);

                if (alerta != null)
                {

                    SICM_ACCIONES_ALERTA accion = await _accionesAlertaRepo.GetByID(6);


                    string contenido = accion.ContenidoEmail;

                    EmailModel email = new EmailModel();

                    contenido = contenido.Replace("[noAlerta]", "Alba-Keneth No." + alerta.codigoAlerta);
                    contenido = contenido.Replace("[noOficio]", alerta.codigoOficio);
                    contenido = contenido.Replace("[observaciones]", model.comentarios);

                    if (model.correosAdicionales != null && model.correosAdicionales != "")
                        email.listCorreos = alerta.correosAdicionales + model.correosAdicionales;
                    else
                        email.listCorreos = alerta.correosAdicionales;

                    email.listDocs = model.adjuntos;
                    email.subjet = "[SICM] Alerta Alba-Keneth Desactivada";
                    email.body = contenido;
                    email.titulo = "Desactivacion de alerta Alba-Keneth";
                    email.saludo = "Señores funcionarios: ";
                    email.datosAlerta = "";

                    await _emailService.sendActionEmailAK(email);

                    alerta.estadoAlerta = 3;
                    alerta.fechaDesactivacion = DateTime.Now;

                    SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                    bitacora.usuario = GlobalsVariables.UserName;
                    bitacora.pais = GlobalsVariables.Pais;
                    bitacora.codigoAccion = accion.Codigo;
                    bitacora.codigoEstado = alerta.estadoAlerta;
                    bitacora.observaciones = model.comentarios;
                    bitacora.fecha = DateTime.Now;
                    bitacora.codigoAlerta = alerta.codigo;
                    bitacora.adjunto = model.adjuntos;

                    alerta.SicmBitacoraAlerta.Add(bitacora);

                    await _alertasRepo.Update(alerta);

                    res.codigo = Constants.OK_code;
                    res.data = new { codigo = Constants.OK_code, message = "Alerta desactivada" };
                }
                else
                {
                    res.codigo = Constants.NoContent_code;
                    res.data = new { message = "Codigo de alerta no encontrado" };
                }

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al intentar desactivar la alerta";
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
                IEnumerable<SICM_ALERTAS> alertas = await _alertasRepo.getAllAlbaKeneth();

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

                res.codigo = Constants.OK_code;
                res.data = new { alertas = resAlestas, total = total };

            }
            catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al intentar crear la alerta";
            }
            return res;
        }
        #endregion

        #endregion

        #region CRUD Bitacora

        public async Task<Response> GetBitacora(int codAlerta)
        {
            Response res = new Response();

            try
            {
                SICM_ALERTAS alerta = _alertasRepo.GetByID(codAlerta).Result;
                if (alerta.tipoAlerta == 1)
                {
                    var bitacora = _mapper.Map<IEnumerable<bitacoraViewModel>>(await _bitacoraRepo.getByCodigoAlerta(codAlerta));
                    res.codigo = 200;
                    res.data = bitacora;
                }
                else
                {
                    res.codigo = 204;
                    res.data = new { codigo = 204, message = "alerta no encontrada" };
                }

                    

            }
            catch (Exception ex)
            {
                res.codigo = 500;
                res.message = ex.Message;
            }
            return res;
        }

        #endregion

        #region CRUD Boletines

        #region Delete Boletin
        public async Task<Response> DeleteBoletin(int codBoletin)
        {
            Response res = new Response();
            try
            {
                SICM_BOLETINES boletin = _boletinesRepo.GetByID(codBoletin).Result;

                await _boletinesRepo.Delete(codBoletin);

                SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                bitacora.usuario = GlobalsVariables.UserName;
                bitacora.pais = GlobalsVariables.Pais;
                bitacora.codigoEstado = 1;
                bitacora.codigoAccion = 7;
                bitacora.observaciones = "Se elimino el boletín de: " + boletin.primerNombre + " " + boletin.primerApellido;
                bitacora.fecha = DateTime.Now;
                bitacora.codigoAlerta = boletin.codigoAlerta;

                await _bitacoraRepo.Add(bitacora);


                res.codigo = 200;
                res.data = new { codigo = 200, message = "Boletín eliminado con éxito" };

            }
            catch (Exception ex)
            {
                res.codigo = 500;
                res.message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Add Boletin
        public async Task<Response> AddBoletin(BoletinAK_DTO boletin, int codAlerta)
        {
            Response res = new Response();
            try
            {
                SICM_BOLETINES newBoletin = _mapper.Map<SICM_BOLETINES>(boletin);

                SICM_ALERTAS alerta = _alertasRepo.GetByID(codAlerta).Result;

                if (alerta != null)
                {

                    newBoletin.codigoEstatus = 1;
                    newBoletin.codigoSituacion = 9;
                    newBoletin.codigoAlerta = codAlerta;
                    newBoletin.tipoCeja = 999;
                    newBoletin.tipoNariz = 999;

                    await _boletinesRepo.Add(newBoletin);

                    SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                    bitacora.usuario = GlobalsVariables.UserName;
                    bitacora.pais = GlobalsVariables.Pais;
                    bitacora.codigoAlerta = codAlerta;
                    bitacora.codigoEstado = alerta.estadoAlerta;
                    bitacora.codigoAccion = 8;
                    bitacora.observaciones = "Se agrego el boletín de " + boletin.primerNombre + " " + boletin.primerApellido;
                    bitacora.fecha = DateTime.Now;

                    await _bitacoraRepo.Add(bitacora);

                    res.codigo = Constants.OK_code;
                    res.data = new { codigo = 200, codigoBoletin = newBoletin.codigoBoletin, message = "Boletín agregado con éxito" };
                }
                else
                {
                    res.codigo = Constants.Error_code;
                    res.message = "Alerta no encontrada";
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

        #region Edit Boletin
        public async Task<Response> EditBoletin(BoletinAK_DTO model, int codBoletin)
        {
            Response res = new Response();
            try
            {
                SICM_BOLETINES boletin = _boletinesRepo.GetByID(codBoletin).Result;

                boletin.codigoBoletin = codBoletin;

                if (model.primerNombre != null && model.primerNombre != "")
                    boletin.primerNombre = model.primerNombre;
                if (model.segundoNombre != null && model.segundoNombre != "")
                    boletin.segundoNombre = model.segundoNombre;
                if (model.tercerNombre != null && model.tercerNombre != "")
                    boletin.tercerNombre = model.tercerNombre;
                if (model.primerApellido != null && model.primerApellido != "")
                    boletin.primerApellido = model.primerApellido;
                if (model.segundoApellido != null && model.segundoApellido != "")
                    boletin.segundoApellido = model.segundoApellido;
                if (model.edad != 0)
                    boletin.edad = model.edad;
                if (model.fechaHora != null)
                    boletin.fechaHora = model.fechaHora;
                if (model.foto != null && model.foto != "")
                    boletin.foto = model.foto;
                if (model.colorCabello != 0)
                    boletin.colorCabello = model.colorCabello;
                if (model.tipoCabello != 0)
                    boletin.tipoCabello = model.tipoCabello;
                if (model.tamanioCabello != 0)
                    boletin.tamanioCabello = model.tamanioCabello;
                if (model.colorOjos != 0)
                    boletin.colorOjos = model.colorOjos;
                if (model.complexion != 0)
                    boletin.complexion = model.complexion;
                if (model.tez != 0)
                    boletin.tez = model.tez;
                if (model.vestimenta != null && model.vestimenta != "")
                    boletin.vestimenta = model.vestimenta;
                if (model.estatura != 0)
                    boletin.estatura = model.estatura;
                if (model.genero != 0)
                    boletin.genero = model.genero;
                if (model.seniasParticulares != null && model.seniasParticulares != "")
                    boletin.seniasParticulares = model.seniasParticulares;
                if (model.notas != null && model.notas != "")
                    boletin.notas = model.notas;
                if (model.boletin != null && model.boletin != "")
                    boletin.boletin = model.boletin;
                if (!String.IsNullOrEmpty(model.nombrePadre))
                    boletin.nombrePadre = model.nombrePadre;
                if (!String.IsNullOrEmpty(model.nombreMadre))
                    boletin.nombreMadre = model.nombreMadre;
                if (!String.IsNullOrEmpty(model.alias))
                    boletin.alias = model.alias;
                if (!String.IsNullOrEmpty(model.responsable))
                    boletin.responsable = model.responsable;
                if (!String.IsNullOrEmpty(model.observaciones))
                    boletin.observaciones = model.observaciones;

                await _boletinesRepo.Update(boletin);

                SICM_ALERTAS alerta = _alertasRepo.GetByID(boletin.codigoAlerta).Result;

                SICM_BITACORA_ALERTA bitacora = new SICM_BITACORA_ALERTA();

                bitacora.usuario = GlobalsVariables.UserName;
                bitacora.pais = GlobalsVariables.Pais;
                bitacora.codigoAlerta = alerta.codigo;
                bitacora.codigoEstado = alerta.estadoAlerta;
                bitacora.codigoAccion = 9;
                bitacora.observaciones = "Se modifico el boletín de " + boletin.primerNombre + " " + boletin.primerApellido;
                bitacora.fecha = DateTime.Now;

                await _bitacoraRepo.Add(bitacora);

                res.codigo = 200;
                res.data = new { codigo = 200, message = "Boletín modificado con éxito" };

            }
            catch (Exception ex)
            {
                res.codigo = 500;
                res.message = ex.Message;
            }
            return res;
        }
        #endregion

        #endregion


    }
}