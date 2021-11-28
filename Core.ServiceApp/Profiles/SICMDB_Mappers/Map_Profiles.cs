using AutoMapper;
using Core.ServiceApp.DTO.Core_DTOs;
using Core.ServiceApp.DTO.Core_DTOs.Catalogos_DTOS;
using Core.ServiceApp.DTO.Core_DTOs.Request;
using Core.ServiceApp.DTO.Core_DTOs.Responses;
using Core.ServiceApp.ViewModels;
using DataAccess.Entities.SICM_DbEntities;
using DataAccess.Entities.SICM_DbEntities.Catalogs;
using DataAccess.Entities.SICM_DbEntities.Generales;


namespace Core.ServiceApp.Profiles.SICMDB_Mappers
{
    public class Map_Profiles : Profile
    {
        public Map_Profiles()
        {
            //mapeamos el modelo con nuestro DTO
            //mapper SICM_TEZ
            // Source -> Target
        }
    }

    public class TEZ_Profile : Profile
    {
        public TEZ_Profile()
        {
            CreateMap<SICM_TEZ, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_TEZ>();
        }
    }

    public class ACC_NOTIFICACION_Profile : Profile
    {
        public ACC_NOTIFICACION_Profile()
        {
            CreateMap<SICM_ACCIONES_NOTIFICACION, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_ACCIONES_NOTIFICACION>();
        }
    }

    public class Situacion_Alerta : Profile
    {
        public Situacion_Alerta()
        {
            CreateMap<SICM_SITUACION_ALERTA, SITUACION_ALERTA_DTO>();
            CreateMap<SITUACION_ALERTA_DTO, SICM_SITUACION_ALERTA>();
        }
    }

    public class ColorCabello_Profile : Profile
    {
        public ColorCabello_Profile()
        {
            CreateMap<SICM_COLOR_CABELLO, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_COLOR_CABELLO>();
        }
    }
    public class ColorOjos_Profile : Profile
    {
        public ColorOjos_Profile()
        {
            CreateMap<SICM_COLOR_OJOS, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_COLOR_OJOS>();
        }
    }
    public class Complexion_Profile : Profile
    {
        public Complexion_Profile()
        {
            CreateMap<SICM_COMPLEXION, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_COMPLEXION>();
        }
    }
    public class Departamentos_Profile : Profile
    {
        public Departamentos_Profile()
        {
            CreateMap<GLOB_DIVISION, GLOB_DIVISION_DTO>();
            CreateMap<GLOB_DIVISION_DTO, GLOB_DIVISION>();
        }
    }

    public class Municipios_Profile : Profile
    {
        public Municipios_Profile()
        {
            CreateMap<GLOB_CIUDAD, GLOB_CIUDAD_DTO>();
            CreateMap<GLOB_CIUDAD_DTO, GLOB_CIUDAD>();
        }
    }
    public class TamanioCabello_Profile : Profile
    {
        public TamanioCabello_Profile()
        {
            CreateMap<SICM_TAMANIO_CABELLO, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_TAMANIO_CABELLO>();
        }
    }
    public class TipoAlerta_Profile : Profile
    {
        public TipoAlerta_Profile()
        {
            CreateMap<SICM_TIPO_ALERTA, TIPO_ALERTA_DTO>();
            CreateMap<TIPO_ALERTA_DTO, SICM_TIPO_ALERTA>();
        }
    }
    public class TipoCabello_Profile : Profile
    {
        public TipoCabello_Profile()
        {
            CreateMap<SICM_TIPO_CABELLO, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_TIPO_CABELLO>();
        }
    }
    public class TipoCeja_Profile : Profile
    {
        public TipoCeja_Profile()
        {
            CreateMap<SICM_TIPO_CEJA, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_TIPO_CEJA>();
        }
    }
    public class TipoNariz_Profile : Profile
    {
        public TipoNariz_Profile()
        {
            CreateMap<SICM_TIPO_NARIZ, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_TIPO_NARIZ>();
        }
    }

    public class Alerta_Profile : Profile
    {
        public Alerta_Profile()
        {
            CreateMap<SICM_ALERTAS, AlertaAlbaKeneth_DTO>();
            CreateMap<AlertaAlbaKeneth_DTO, SICM_ALERTAS>();
        }
    }

    public class AlertasAlbaKeneth_Profile : Profile
    {
        public AlertasAlbaKeneth_Profile()
        {
            CreateMap<SICM_ALERTAS, AlertasAlbaKeneth_ViewModel>();
            CreateMap<AlertasAlbaKeneth_ViewModel, SICM_ALERTAS>();
        }
    }

    public class Misiones_Profile : Profile
    {
        public Misiones_Profile()
        {
            CreateMap<Core.ServiceApp.ViewModels.misionesViewModel, DataAccess.Helpers.ViewModels.misionesViewModel>();
            CreateMap<DataAccess.Helpers.ViewModels.misionesViewModel, Core.ServiceApp.ViewModels.misionesViewModel>();
        }
    }

    public class AlertasAK_Profile : Profile
    {
        public AlertasAK_Profile()
        {
            CreateMap<AlertasIsabelClaudina_ViewModel, SICM_ALERTAS>();
            CreateMap<SICM_ALERTAS, AlertasIsabelClaudina_ViewModel>();
        }
    }

    public class Paises_Profile : Profile
    {
        public Paises_Profile()
        {
            CreateMap<GLOB_PAIS_DTO, GLOB_PAIS>();
            CreateMap<GLOB_PAIS, GLOB_PAIS_DTO>();
        }
    }

    public class boletines_Profile : Profile
    {
        public boletines_Profile()
        {
            CreateMap<SICM_BOLETINES, BoletinAK_DTO>();
            CreateMap<BoletinAK_DTO, SICM_BOLETINES>();
        }
    }

    public class bitacora_Profile : Profile
    {
        public bitacora_Profile()
        {
            CreateMap<SICM_BITACORA_ALERTA, bitacoraViewModel>();
            CreateMap<bitacoraViewModel, SICM_BITACORA_ALERTA>();
        }
    }

    public class searchAlerta_Profile : Profile
    {
        public searchAlerta_Profile()
        {
            CreateMap<SICM_ALERTAS, alertas_searchViewModel>();
            CreateMap<alertas_searchViewModel, SICM_ALERTAS>();
        }
    }

    public class searcBoletin_Profile : Profile
    {
        public searcBoletin_Profile()
        {
            CreateMap<SICM_BOLETINES, boletines_searchViewModel>();
            CreateMap<boletines_searchViewModel, SICM_BOLETINES>();
        }
    }

    public class acciones_Profile : Profile
    {
        public acciones_Profile()
        {
            CreateMap<SICM_ACCIONES_ALERTA, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_ACCIONES_ALERTA>();
        }
    }

    public class situacion_Profile : Profile
    {
        public situacion_Profile()
        {
            CreateMap<SICM_SITUACION_ALERTA, CatalogoGenerico_DTO>();
            CreateMap<CatalogoGenerico_DTO, SICM_SITUACION_ALERTA>();
        }
    }

    public class acciongenerico_Profile : Profile
    {
        public acciongenerico_Profile()
        {
            CreateMap<SICM_ACCIONES_NOTIFICACION, CatalogoGenerico_DTO>();
            CreateMap<CatalogoGenerico_DTO, SICM_ACCIONES_NOTIFICACION>();
        }
    }

    public class estatusgenerico_Profile : Profile
    {
        public estatusgenerico_Profile()
        {
            CreateMap<SICM_ESTATUS_ALERTA, CatalogoGenerico_DTO>();
            CreateMap<CatalogoGenerico_DTO, SICM_ESTATUS_ALERTA>();
        }
    }

    public class estatus_Profile : Profile
    {
        public estatus_Profile()
        {
            CreateMap<SICM_ESTATUS_ALERTA, CatalogoGenerico_ViewModel>();
            CreateMap<CatalogoGenerico_ViewModel, SICM_ESTATUS_ALERTA>();
        }
    }

    public class estatusDTO_Profile : Profile
    {
        public estatusDTO_Profile()
        {
            CreateMap<SICM_ESTATUS_ALERTA, CatalogoEstatusSituacion_DTO>();
            CreateMap<CatalogoEstatusSituacion_DTO, SICM_ESTATUS_ALERTA>();
        }
    }

    public class accionNotificacion_Profile : Profile
    {
        public accionNotificacion_Profile()
        {
            CreateMap<SICM_ACCIONES_NOTIFICACION, CatalogoEstatusSituacion_DTO>();
            CreateMap<CatalogoEstatusSituacion_DTO, SICM_ACCIONES_NOTIFICACION>();
        }
    }

    public class situacionAlerta_Profile : Profile
    {
        public situacionAlerta_Profile()
        {
            CreateMap<CatalogoEstatusSituacion_DTO, SICM_SITUACION_ALERTA>();
            CreateMap<SICM_SITUACION_ALERTA, CatalogoEstatusSituacion_DTO>();
        }
    }

}
