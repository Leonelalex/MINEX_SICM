using Core.ServiceApp.DTO.Core_DTOs;
using Core.ServiceApp.DTO.Core_DTOs.Catalogos_DTOS;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Entities.SICM_DbEntities.Generales;
using System.Collections.Generic;

namespace Core.ServiceApp.ViewModels
{
    public partial class CatalogosViewModel
    {
        public IEnumerable<CatalogoGenerico_ViewModel> colorCabello { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> colorOjos { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> complexion { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> tamanioCabello { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> tez { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> tipoCabello { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> tipoCeja { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> tipoNariz { get; set; }
        public IEnumerable<GLOB_DIVISION_DTO> departamentos { get; set; }
        public IEnumerable<GLOB_CIUDAD_DTO> municipios { get; set; }
        public IEnumerable<GLOB_PAIS_DTO> paises { get; set; }
        public List<misionesViewModel> misiones { get; set; }
        public IEnumerable<GLOB_GENERO> generos { get; set; }
        public IEnumerable<SICM_ESTADOS_ALERTA> estadosAlerta { get; set; }
        public IEnumerable<CatalogoEstatusSituacion_DTO> situacion_alerta { get; set; }
        public IEnumerable<CatalogoEstatusSituacion_DTO> estatusAlerta { get; set; }
        public IEnumerable<CatalogoGenerico_ViewModel> accionesAlerta { get; set; }
        public IEnumerable<CatalogoEstatusSituacion_DTO> accionesNotificacion { get; set; }
    }
}
