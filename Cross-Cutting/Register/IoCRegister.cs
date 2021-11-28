using Core.ServiceApp.Services;
using Core.ServiceApp.Services.ServiceContracts;
using Core.ServiceApp.Utils;
using Core.ServiceApp.Utils.UtilContracts;
using DataAccess.DBContexts;
using DataAccess.DBContexts.DBContracts;
using DataAccess.SICM_Repositories;
using DataAccess.SICM_Repositories.RepositoriesContracts;
using External.ServiceApp.Services;
using External.ServiceApp.Services.ServicesContracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cross_Cutting.Register
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            services.AddScoped<ISICM_DBContext, SICM_DBContext>();
            AddRegisterRepositories(services);
            AddRegisterServices(services);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        private static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICatalogosService, CatalogosService>();
            services.AddTransient<IAlbaKenethService, AlbaKenethService>();
            services.AddTransient<IIsabelClaudinaService, IsabelClaudinaService>();
            services.AddTransient<ISearchModuleService, SearchModuleService>();
            services.AddTransient<SSOService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IActionService, ActionsService>();
            services.AddTransient<ILogUtils, LogUtils>();
            services.AddTransient<IMPService, MPService>();
            services.AddTransient<IResportesService, ReportesServie>();
            return services;
        }

        private static IServiceCollection AddRegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISICM_TEZ_REPO, SICM_TEZ_REPO>();
            services.AddScoped<ISICM_COLOR_CABELLO_REPO, SICM_COLOR_CABELLO_REPO>();
            services.AddScoped<ISICM_COLOR_OJOS_REPO, SICM_COLOR_OJOS_REPO>();
            services.AddScoped<ISICM_COMPLEXION_REPO, SICM_COMPLEXION_REPO>();
            services.AddScoped<ISICM_TAMANIO_CABELLO_REPO, SICM_TAMANIO_CABELLO_REPO>();
            services.AddScoped<ISICM_TIPO_CEJA_REPO, SICM_TIPO_CEJA_REPO>();
            services.AddScoped<ISICM_TIPO_NARIZ_REPO, SICM_TIPO_NARIZ_REPO>();
            services.AddScoped<ISICM_TIPO_CABELLO_REPO, SICM_TIPO_CABELLO_REPO>();
            services.AddScoped<ISICM_ESTADOS_ALERTAS_REPO, SICM_ESTADOS_ALERTA_REPO>();
            services.AddScoped<ISICM_BITACORA_ALERTA_REPO, SICM_BITACORA_ALERTA_REPO>();
            services.AddScoped<ISICM_ALERTA_MISIONES_REPO, SICM_ALERTA_MISIONES_REPO>();
            services.AddScoped<ISICM_BOLETINES_REPO, SICM_BOLETINES_REPO>();
            services.AddScoped<ISICM_SITUACION_ALERTA_REPO, SICM_SITUACION_ALERTA_REPO>();
            services.AddScoped<ISICM_ACCIONES_ALERTA_REPO, SICM_ACCIONES_ALERTA_REPO>();
            services.AddScoped<ISICM_ESTATUS_ALERTA_REPO, SICM_ESTATUS_ALERTA_REPO>();
            services.AddScoped<ISICM_ACCIONES_NOTIFICACION_REPO, SICM_ACCIONES_NOTIFICACION_REPO>();

            services.AddScoped<IDEPARTAMENTOS_REPO, DEPARTAMENTOS_REPO>();
            services.AddScoped<IMUNICIPIOS_REPO, MUNICIPIOS_REPO>();

            services.AddScoped<ISICM_ALERTAS_REPO, SICM_ALERTAS_REPO>();
            services.AddScoped<IGLOB_MISIONES_EXTERIOR_REPO, GLOB_MISIONES_EXTERIOR_REPO>();
            services.AddScoped<IGLOB_TIPO_MISION_REPO, GLOB_TIPO_MISION_REPO>();
            services.AddScoped<IGLOB_PAIS_REPO, GLOB_PAIS_REPO>();
            services.AddScoped<IGLOB_GENERO_REPO, GLOB_GENERO_REPO>();

            services.AddScoped<ISICM_SYSLOG_REPO, SICM_SYSLOG_REPO>();
            services.AddScoped<ISICM_PARAMETROS_GENERALES_REPO, SICM_PARAMETROS_GENERALES_REPO>();

            services.AddScoped<ISICM_REPORTES_REPO, SICM_REPORTES_REPO>();

            return services;
        }
    }
}
