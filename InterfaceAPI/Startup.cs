
using Cross_Cutting.Register;
using DataAccess.DBContexts;
using External.ServiceApp.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace InterfaceApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            GlobalsVariables.EnvirommentSSO_Service = Configuration.GetSection("ConectionSSO_Service").Value;
            GlobalsVariables.SistemId = configuration.GetSection("SistemIdSICM").Value;
            GlobalsVariables.CodigoPermiso = configuration.GetSection("CodigoPermisoAlertas").Value;
            GlobalsVariables.EnviromentConnApiEmailAttach = configuration.GetSection("ConnApiEmailAttach").Value;
            GlobalsVariables.EnviromentConnApiEmail = configuration.GetSection("ConnApiEmail").Value;
            GlobalsVariables.RouteTemplateIC = configuration.GetSection("RouteTemplateIC").Value;
            GlobalsVariables.RouteTemplateAK = configuration.GetSection("RouteTemplateAK").Value;

            GlobalsVariables.ICOficios_Path = configuration.GetSection("RouteServerICoficios").Value;
            GlobalsVariables.AKOficios_Path = configuration.GetSection("RouteServerAKoficios").Value;
            GlobalsVariables.AKPics_Path = configuration.GetSection("RouteServerFotosAK").Value;
            GlobalsVariables.ICPics_Path = configuration.GetSection("RouteServerFotosIC").Value;
            GlobalsVariables.ICDocs_Path = configuration.GetSection("RouteServerDocIC").Value;
            GlobalsVariables.AKDocs_Path = configuration.GetSection("RouteServerDocAK").Value;


            #region seteo de variables para Servicio MP
            GlobalsVariables.KeyMP = configuration.GetSection("KeyMP").Value;
            GlobalsVariables.SystemMPDesarrollo = configuration.GetSection("SystemMPDesarrollo").Value;
            GlobalsVariables.UsernameMP = configuration.GetSection("UsernameMP").Value;
            GlobalsVariables.PasswordMP = configuration.GetSection("PasswordMP").Value;
            GlobalsVariables.ConectionMPAuth = configuration.GetSection("ConectionMPAuth").Value;
            GlobalsVariables.ConectionMPCatalogo = configuration.GetSection("ConectionMPCatalogo").Value;
            GlobalsVariables.ConectionMPArchivo = configuration.GetSection("ConectionMPArchivo").Value;
            #endregion

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema de Informacion Consulares y Migratorios", Version = "1.0.0", Description = "Modulo de Alertas Alba-Keneth e Isabel-Claudina" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            //agregamos la coneccion necesaria
            services.AddDbContext<SICM_DBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Registrar los servicios
            IoCRegister.AddRegistration(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(o => o.AddPolicy("EnableCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("SessionToken")
                       .WithExposedHeaders("SystemId");
            }));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers();
            services.AddHttpClient();//agregamos el httpclient

            services.Configure<ForwardedHeadersOptions>(options => {
                options.KnownProxies.Add(IPAddress.Parse("192.168.11.37"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InterfaceApi.API v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("EnableCorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");

                // custom CSS
                c.InjectStylesheet("/swagger-ui/custom.css");
            });
        }


    }
}
