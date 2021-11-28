using AutoMapper;
using Core.ServiceApp.DTO.Core_DTOs;
using Core.ServiceApp.DTO.Core_DTOs.Catalogos_DTOS;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.ViewModels;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.SICM_Repositories;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services
{
    public class CatalogosService : ICatalogosService
    {
        private readonly IMapper _mapper;

        public readonly IDEPARTAMENTOS_REPO _dEPARTAMENTOS_REPO;
        public readonly IMUNICIPIOS_REPO _mUNICIPIOS_REPO;
        public readonly ISICM_TIPO_NARIZ_REPO _tIPO_NARIZ_REPO;
        public readonly ISICM_TIPO_CEJA_REPO _tIPO_CEJA_REPO;
        public readonly ISICM_TIPO_CABELLO_REPO _tIPO_CABELLO_REPO;
        public readonly ISICM_TEZ_REPO _tEZ_REPO;
        public readonly ISICM_TAMANIO_CABELLO_REPO _tAMANIO_CABELLO_REPO;
        public readonly ISICM_COMPLEXION_REPO _cOMPLEXION_REPO;
        public readonly ISICM_COLOR_OJOS_REPO _cOLOR_OJOS_REPO;
        public readonly ISICM_COLOR_CABELLO_REPO _cOLOR_CABELLO_REPO;
        public readonly IGLOB_MISIONES_EXTERIOR_REPO _MISIONES_EXTERIOR_REPO;
        public readonly IGLOB_PAIS_REPO _PAIS_REPO;
        public readonly IGLOB_GENERO_REPO _GENERO_REPO;
        public readonly ISICM_ESTADOS_ALERTAS_REPO _ESTADOS_ALERTAS_REPO;
        public readonly ISICM_SITUACION_ALERTA_REPO _SITUACION_ALERTA_REPO;
        public readonly ISICM_ESTATUS_ALERTA_REPO _ESTATUS_ALERTA_REPO;
        public readonly ISICM_ACCIONES_ALERTA_REPO _ACCIONES_ALERTA_REPO;
        public readonly ISICM_ACCIONES_NOTIFICACION_REPO _ACCIONES_NOTIFICACION;
        public readonly ISICM_PARAMETROS_GENERALES_REPO _PARAMETROS_GENERALES;

        #region constructor
        public CatalogosService(IDEPARTAMENTOS_REPO dEPARTAMENTOS_REPO, IMUNICIPIOS_REPO mUNICIPIOS_REPO, ISICM_TIPO_NARIZ_REPO tIPO_NARIZ_REPO, 
            ISICM_TIPO_CEJA_REPO tIPO_CEJA_REPO, ISICM_TIPO_CABELLO_REPO tIPO_CABELLO_REPO, ISICM_TEZ_REPO tEZ_REPO, ISICM_TAMANIO_CABELLO_REPO tAMANIO_CABELLO_REPO, 
            ISICM_COMPLEXION_REPO cOMPLEXION_REPO, ISICM_COLOR_OJOS_REPO cOLOR_OJOS_REPO, ISICM_COLOR_CABELLO_REPO cOLOR_CABELLO_REPO,
            IGLOB_MISIONES_EXTERIOR_REPO misiones_exterior_repo, IGLOB_PAIS_REPO pais_repo, IGLOB_GENERO_REPO _genero_repo, ISICM_ESTADOS_ALERTAS_REPO _estados_alerta_repo,
            ISICM_SITUACION_ALERTA_REPO situacion_alerta_repo, ISICM_ESTATUS_ALERTA_REPO ESTATUS_ALERTA_REPO, ISICM_ACCIONES_ALERTA_REPO ACCIONES_ALERTA_REPO,
            ISICM_ACCIONES_NOTIFICACION_REPO ACCIONES_NOTIFICACION, ISICM_PARAMETROS_GENERALES_REPO PARAMETROS_GENERALES, IMapper mapper)
        {
            _dEPARTAMENTOS_REPO = dEPARTAMENTOS_REPO;
            _mUNICIPIOS_REPO = mUNICIPIOS_REPO;
            _tIPO_NARIZ_REPO = tIPO_NARIZ_REPO;
            _tIPO_CEJA_REPO = tIPO_CEJA_REPO;
            _tIPO_CABELLO_REPO = tIPO_CABELLO_REPO;
            _tEZ_REPO = tEZ_REPO;
            _tAMANIO_CABELLO_REPO = tAMANIO_CABELLO_REPO;
            _cOMPLEXION_REPO = cOMPLEXION_REPO;
            _cOLOR_OJOS_REPO = cOLOR_OJOS_REPO;
            _cOLOR_CABELLO_REPO = cOLOR_CABELLO_REPO;
            _mapper = mapper;
            _MISIONES_EXTERIOR_REPO = misiones_exterior_repo;
            _PAIS_REPO = pais_repo;
            _GENERO_REPO = _genero_repo;
            _ESTADOS_ALERTAS_REPO = _estados_alerta_repo;
            _SITUACION_ALERTA_REPO = situacion_alerta_repo;
            _ESTATUS_ALERTA_REPO = ESTATUS_ALERTA_REPO;
            _ACCIONES_ALERTA_REPO = ACCIONES_ALERTA_REPO;
            _ACCIONES_NOTIFICACION = ACCIONES_NOTIFICACION;
            _PARAMETROS_GENERALES = PARAMETROS_GENERALES;
        }
        #endregion

        #region Get All Catalogos
        public async Task<Response> GetAllCatalogos()
        {
            Response res = new Response();
            try
            {
                var dep = await _dEPARTAMENTOS_REPO.getByPais(106);
                //var paises = await _PAIS_REPO.GetAll();

                CatalogosViewModel catalogos = new CatalogosViewModel
                {
                    departamentos = _mapper.Map<IEnumerable<GLOB_DIVISION_DTO>>(dep),
                    municipios = _mapper.Map<IEnumerable<GLOB_CIUDAD_DTO>>(_mUNICIPIOS_REPO.getByDivisionesAsync(dep)),
                    colorCabello = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _cOLOR_CABELLO_REPO.GetAllDesc()),
                    colorOjos = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _cOLOR_OJOS_REPO.GetAllDesc()),
                    complexion = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _cOMPLEXION_REPO.GetAllDesc()),
                    tamanioCabello = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _tAMANIO_CABELLO_REPO.GetAllDesc()),
                    tipoCabello = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _tIPO_CABELLO_REPO.GetAllDesc()),
                    tipoCeja = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _tIPO_CEJA_REPO.GetAllDesc()),
                    tipoNariz = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _tIPO_NARIZ_REPO.GetAllDesc()),
                    tez = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _tEZ_REPO.GetAllDesc()),
                    paises = _mapper.Map<IEnumerable<GLOB_PAIS_DTO>>(await _PAIS_REPO.getPaisesConMision()),
                    misiones = _mapper.Map<List<misionesViewModel>>(await _MISIONES_EXTERIOR_REPO.getMisiones()),
                    generos = await _GENERO_REPO.GetAll(),
                    estadosAlerta = await _ESTADOS_ALERTAS_REPO.GetAll(),
                    situacion_alerta = _mapper.Map<IEnumerable<CatalogoEstatusSituacion_DTO>>(await _SITUACION_ALERTA_REPO.GetAll()),
                    estatusAlerta = _mapper.Map<IEnumerable<CatalogoEstatusSituacion_DTO>>( await _ESTATUS_ALERTA_REPO.GetAll()),
                    accionesAlerta = _mapper.Map<IEnumerable<CatalogoGenerico_ViewModel>>(await _ACCIONES_ALERTA_REPO.GetAll()),
                    accionesNotificacion = _mapper.Map<IEnumerable<CatalogoEstatusSituacion_DTO>>(await _ACCIONES_NOTIFICACION.GetAll())
                };

                res.codigo = Constants.OK_code;
                res.data = catalogos;

            }catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al intentar obtener los catalogos";
            }
            return res;
        }
        #endregion

        #region CRUD Catalogo Situacion

        #region Get Situacion
        public async Task<Response> GetAllSituacion()
        {
            Response res = new Response();
            try
            {
                res.data = await _SITUACION_ALERTA_REPO.GetAll();
                res.codigo = Constants.OK_code;

            }catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error obteniendo el catalogo de situaciones especiales";
            }
            return res;
        }
        #endregion

        #region Post Situacion
        public async Task<Response> PostSituacion(CatalogoGenerico_DTO model)
        {
            Response res = new Response();
            try
            {

                SICM_SITUACION_ALERTA situacion = _mapper.Map<SICM_SITUACION_ALERTA>(model);
                situacion.Activo = true;

                await _SITUACION_ALERTA_REPO.Add(situacion);

                res.codigo = 200;
                res.data = new { codigoSituacion = situacion.Codigo, message = "Se agregó la situación con éxito" };

            }catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al intentar guardar la situación especial";
            }
            return res;
        }
        #endregion

        #region Put Situacion
        public async Task<Response> PutSituacion(CatalogoGenerico_DTO model, int codSituacion)
        {
            Response res = new Response();
            try
            {
                SICM_SITUACION_ALERTA situacion = _SITUACION_ALERTA_REPO.GetByID(codSituacion).Result;
                if(situacion != null)
                {
                    if (!String.IsNullOrEmpty(model.nombre))
                        situacion.Nombre = model.nombre;
                    if (!String.IsNullOrEmpty(model.descripcion))
                        situacion.Descripcion = model.descripcion;
                    if (model.activo != situacion.Activo)
                        situacion.Activo = model.activo;
                    if (model.isAK != situacion.isAK)
                        situacion.isAK = model.isAK;
                    if (model.isIC != situacion.isIC)
                        situacion.isIC = model.isIC;

                    await _SITUACION_ALERTA_REPO.Update(situacion);

                    res.codigo = Constants.OK_code;
                    res.data = new { message = "Situación actualizada con éxito" };
                }
                else
                {
                    res.codigo = Constants.NoContent_code;
                    res.message = "El código de situación no existe";
                }
            }catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al intentar editar la situación especial";
            }
            return res;
        }
        #endregion

        #region Delete Situacion
        public async Task<Response> DeleteSituacion(int codSituacion)
        {
            Response res = new Response();
            try
            {
                SICM_SITUACION_ALERTA situacion = _SITUACION_ALERTA_REPO.GetByID(codSituacion).Result;

                situacion.Activo = false;

                await _SITUACION_ALERTA_REPO.Update(situacion);

                res.codigo = Constants.OK_code;
                res.data = new { message = "Situación desactivada con éxito" };

            }catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al intentar desactivar la situación especial";
            }
            return res;
        }
        #endregion

        #endregion

        #region CRUD Catalogos Estatus

        #region Get Estatus
        public async Task<Response> GetAllEstatus()
        {
            Response res = new Response();
            try
            {
                res.data = await _ESTATUS_ALERTA_REPO.GetAll();
                res.codigo = Constants.OK_code;

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error obteniendo el catalogo de estados de boletin";
            }
            return res;
        }
        #endregion

        #region Post Estatus
        public async Task<Response> PostEstatus(CatalogoGenerico_DTO model)
        {
            Response res = new Response();
            try
            {
                SICM_ESTATUS_ALERTA estatus = _mapper.Map<SICM_ESTATUS_ALERTA>(model);
                estatus.activo = true;

                await _ESTATUS_ALERTA_REPO.Add(estatus);

                res.codigo = Constants.OK_code;
                res.data = new { codigoEstatus = estatus.codigo, message = "Se agregó el estado de boletín con éxito" };
            }
            catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al intentar guardas el estado de boletin";
            }
            return res;
        }
        #endregion

        #region Put Estatus
        public async Task<Response> PutEstatus(CatalogoGenerico_DTO model, int codEstatus)
        {
            Response res = new Response();
            try
            {
                SICM_ESTATUS_ALERTA estatus = _ESTATUS_ALERTA_REPO.GetByID(codEstatus).Result;

                if (!String.IsNullOrEmpty(model.nombre))
                    estatus.nombre = model.nombre;
                if (!string.IsNullOrEmpty(model.descripcion))
                    estatus.descripcion = model.descripcion;
                if (model.activo != estatus.activo)
                    estatus.activo = model.activo;
                if (model.isAK != estatus.isAK)
                    estatus.isAK = model.isAK;
                if (model.isIC != estatus.isIC)
                    estatus.isIC = model.isIC;

                await _ESTATUS_ALERTA_REPO.Update(estatus);

                res.codigo = Constants.OK_code;
                res.data = new { message = "Estado de boletín actualizado con éxito" };

            }catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al intentar editar el estado de boletin";
            }
            return res;
        }
        #endregion

        #region Delete Estatus
        public async Task<Response> DeleteEstatus(int codEstatus)
        {
            Response res = new Response();
            try
            {
                SICM_ESTATUS_ALERTA estatus = _ESTATUS_ALERTA_REPO.GetByID(codEstatus).Result;

                estatus.activo = false;

                await _ESTATUS_ALERTA_REPO.Update(estatus);

                res.codigo = Constants.OK_code;
                res.data = new { message = "Estado de boletín desactivado con éxito" };

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al intentar desactivar el estado de boletin";
            }
            return res;
        }
        #endregion

        #endregion

        #region CRUD Catalogo Accion Notificacion

        #region Get Acciones Notificacion
        public async Task<Response> GetAllAccionesNotificacion()
        {
            Response res = new Response();
            try
            {
                res.data = await _ACCIONES_NOTIFICACION.GetAll();
                res.codigo = Constants.OK_code;

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error obteniendo el catalogo de acciones";
            }
            return res;
        }
        #endregion

        #region Post Accion Notificacion
        public async Task<Response> PostAccionNotificacion(CatalogoGenerico_DTO model)
        {
            Response res = new Response();
            try
            {
                SICM_ACCIONES_NOTIFICACION accion = _mapper.Map<SICM_ACCIONES_NOTIFICACION>(model);
                accion.activo = true;

                await _ACCIONES_NOTIFICACION.Add(accion);

                res.codigo = Constants.OK_code;
                res.data = new { codigoAccion = accion.codigo, message = "La acción se agregó con éxito" };
            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al guardar la acción";
            }
            return res;
        }
        #endregion

        #region Put Accion Notificacion
        public async Task<Response> PutAccionNotificacion(CatalogoGenerico_DTO model, int codAccion)
        {
            Response res = new Response();
            try
            {
                SICM_ACCIONES_NOTIFICACION accion = _ACCIONES_NOTIFICACION.GetByID(codAccion).Result;

                if (!String.IsNullOrEmpty(model.nombre))
                    accion.nombre = model.nombre;
                if (!String.IsNullOrEmpty(model.descripcion))
                    accion.descripcion = model.descripcion;
                if (model.activo != accion.activo)
                    accion.activo = model.activo;
                if (model.isAK != accion.isAK)
                    accion.isAK = model.isAK;
                if (model.isIC != accion.isIC)
                    accion.isIC = model.isIC;

                await _ACCIONES_NOTIFICACION.Update(accion);

                res.codigo = Constants.OK_code;
                res.data = new { message = "Acción notificación actualizada con éxito" };

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al editar la acción";
            }
            return res;
        }
        #endregion

        #region Delete Accion Notificaccion
        public async Task<Response> DeleteAccionNotificacion(int codAccion)
        {
            Response res = new Response();
            try
            {
                SICM_ACCIONES_NOTIFICACION accion = _ACCIONES_NOTIFICACION.GetByID(codAccion).Result;

                accion.activo = false;

                await _ACCIONES_NOTIFICACION.Update(accion);

                res.codigo = Constants.OK_code;
                res.data = new { message = "Acción desactivada con éxito" };

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al desactivar la acción";
            }
            return res;
        }
        #endregion

        #endregion

        #region CRUD Parametros Generales

        #region GET Parametros Generales
        public async Task<Response> GetAllParametros()
        {
            Response res = new Response();
            try
            {
                res.data = await _PARAMETROS_GENERALES.GetAll();
                res.codigo = Constants.OK_code;

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error obteniendo los parametros del sistema";
            }
            return res;
        }
        #endregion

        #region PUT Parametros Generales
        public async Task<Response> PutParametrosGenerales(ParametrosGenerales_DTO model, int codParam)
        {
            Response res = new Response();
            try
            {
                SICM_PARAMETROS_GENERALES param = _PARAMETROS_GENERALES.GetByID(codParam).Result;

                if (!String.IsNullOrEmpty(model.valor))
                    param.valor = model.valor;


                await _PARAMETROS_GENERALES.Update(param);

                res.codigo = Constants.OK_code;
                res.data = new { message = "Parametro editado con éxito" };

            }
            catch (Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Error al editar el parametro";
            }
            return res;
        }
        #endregion

        #endregion
    }
}