using AutoMapper;
using Core.ServiceApp.DTO.Core_DTOs.Responses;
using Core.ServiceApp.Helpers;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.ViewModels;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Helpers;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ServiceApp.Services
{
    public class SearchModuleService : ISearchModuleService
    {
        private readonly IMapper _mapper;
        private readonly ISICM_BOLETINES_REPO _boletinesRepo;
        private readonly ISICM_ALERTAS_REPO _alertasRepo;
        public SearchModuleService(IMapper mapper, ISICM_BOLETINES_REPO boletinesRepo, ISICM_ALERTAS_REPO alertasRepo)
        {
            _mapper = mapper;
            _boletinesRepo = boletinesRepo;
            _alertasRepo = alertasRepo;
        }

        #region Busqueda Local
        public async Task<Response> buscarPersonaLocal(persona_ViewModel persona, PaginationFilter pagefilter)
        {
            Response res = new Response();
            try
            {
                if (persona.cui != null) {
                    SICM_BOLETINES boletin = new SICM_BOLETINES();

                    boletin = await _boletinesRepo.FindFirstOrDefault(
                        x => x.cui == persona.cui);

                    if(boletin != null)
                    {
                        List<alertas_searchViewModel> alertas = new List<alertas_searchViewModel>();
                        alertas_searchViewModel alerta = _mapper.Map<alertas_searchViewModel>(await _alertasRepo.GetByID(boletin.codigoAlerta));
                        alerta.boletines = _mapper.Map<boletines_searchViewModel>(boletin);

                        alertas.Add(alerta);

                        res.codigo = Constants.OK_code;
                        res.data = alertas;
                    }
                    else
                    {
                        res.codigo = Constants.NoContent_code;
                        res.message = "No existen coincidencias";
                    }
                }
                else
                {
                    IEnumerable<SICM_BOLETINES> boletines = await _boletinesRepo.GetAll();

                    if(persona.primerNombre != null && persona.primerNombre != "")
                        boletines = boletines.Where(x => x.primerNombre.Contains(persona.primerNombre.ToUpper()));

                    if (persona.segundoNombre != null && persona.segundoNombre != "")
                        boletines = boletines.Where(x => x.segundoNombre.Contains(persona.segundoNombre.ToUpper()));

                    if (persona.tercerNombre != null && persona.tercerNombre != "")
                        boletines = boletines.Where(x => x.tercerNombre.Contains(persona.tercerNombre.ToUpper()));

                    if (persona.primerApellido != null && persona.primerApellido != "")
                        boletines = boletines.Where(x => x.primerApellido.Contains(persona.primerApellido.ToUpper()));

                    if (persona.segundoApellido != null && persona.segundoApellido != "")
                        boletines = boletines.Where(x => x.segundoApellido.Contains(persona.segundoApellido.ToUpper()));

                    int total = boletines.Count();
                    boletines = boletines.Skip((pagefilter.PageNumber - 1) * pagefilter.PageSize).Take(pagefilter.PageSize);

                    if (boletines.Count() > 0)
                    {
                        List<alertas_searchViewModel> alertas = new List<alertas_searchViewModel>();

                        foreach (SICM_BOLETINES bol in boletines) {

                            alertas_searchViewModel alerta =  _mapper.Map<alertas_searchViewModel>(await _alertasRepo.GetByID(bol.codigoAlerta));
                            alerta.boletines = _mapper.Map<boletines_searchViewModel>(bol);

                            alertas.Add(alerta);
                        }

                        alertas = alertas.OrderByDescending(x => x.fechaActivacion).ToList();

                        res.codigo = Constants.OK_code;
                        res.data = new { alertas = alertas, total = total};
                    }
                    else
                    {
                        res.codigo = Constants.NoContent_code;
                        res.message = "No existen coincidencias";
                    }
                }
                return res;
            }
            catch (Exception ex) {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al buscar a la persona";
            }
            return res;
        }
        #endregion

        #region Buscar Alerta por Codigo
        public async Task<Response> buscarAlertaporCodigo(string codigoAlerta)
        {
            Response res = new Response();
            try
            {

               AlertasAlbaKeneth_ViewModel alerta = _mapper.Map<AlertasAlbaKeneth_ViewModel>(await _alertasRepo.getAlertaByCodigo(codigoAlerta));

                if(alerta != null)
                {
                    res.codigo = Constants.OK_code;
                    res.data = alerta;
                }
                else
                {
                    res.codigo = Constants.NoContent_code;
                    res.data = new { message = "No se encontró el código de la alerta" };
                }

            }
            catch(Exception ex)
            {
                res.codigo = Constants.Error_code;
                res.innerError = ex.InnerException.Message;
                res.message = "Hubo un error al buscar a la persona";
            }
            return res;
        }
        #endregion
    }
}
