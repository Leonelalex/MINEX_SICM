using External.ServiceApp.DTOs;
using External.ServiceApp.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static External.ServiceApp.Services.MPService;

namespace External.ServiceApp.Services
{
    public class CatalogoService
    {
        public async Task<object> getCatalogos()
        {
            try
            {
                string url = GlobalsVariables.ConectionMPCatalogo;
                MPService getservice = new MPService();

                tokenvalid respuesta = await getservice.getTokenMP();

                using (var httpclient = new HttpClient())
                {
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
                    {
                        requestMessage.Headers.Add("system", "MINEX");
                        requestMessage.Headers.Add("cui", "134787987897");
                        requestMessage.Headers.Add("Authorization", "Bearer " + respuesta.accessToken);
                        var response = await httpclient.SendAsync(requestMessage);

                        if (!response.IsSuccessStatusCode)
                        {
                            return "Fallo conexión a servicio";
                        }
                        else
                        {
                            var contenidoString = await response.Content.ReadAsStringAsync();
                            return contenidoString;
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                throw wex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<mpResponse> getCatalogosID(string idCatalogo)
        {
            try
            {
                string url = GlobalsVariables.ConectionMPCatalogo;

                MPService getservice = new MPService();

                tokenvalid respuesta = await getservice.getTokenMP();

                mpResponse data = new mpResponse();

                using (var httpclient = new HttpClient())
                {
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + idCatalogo))
                    {
                        requestMessage.Headers.Add("system", "MINEX");
                        requestMessage.Headers.Add("cui", "134787987897");
                        requestMessage.Headers.Add("Authorization", "Bearer " + respuesta.accessToken);
                        var response = await httpclient.SendAsync(requestMessage);

                        if (!response.IsSuccessStatusCode)
                        {
                            return data;
                        }
                        else
                        {
                            var contenido = await response.Content.ReadAsStringAsync();

                            data = JsonSerializer.Deserialize<mpResponse>(contenido,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                            return data;
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                throw wex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<newCatalogo> mpCatalogos(string entidad, int idRegister)
        {
            newCatalogo respuesta = new newCatalogo();
            switch (entidad)
            {
                case ConstantsEx.SICM_COLOR_CABELLO:

                    mpResponse res = await getCatalogosID(ConstantsEx.color_cabello);

                    foreach (var item in res.data.documentos)
                    {
                        if (item.id_color_cabello == idRegister)
                        {
                            respuesta.CODIGO = item.id_color_cabello;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }
                    return respuesta;

                case ConstantsEx.SICM_COLOR_OJOS:

                    mpResponse resc = await getCatalogosID(ConstantsEx.tipo_ojo);

                    foreach (var item in resc.data.documentos)
                    {
                        if (item.id_tipo_ojo == idRegister)
                        {
                            respuesta.CODIGO = item.id_tipo_ojo;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }
                    return respuesta;
                case ConstantsEx.SICM_COMPLEXION:

                    mpResponse rescom = await getCatalogosID(ConstantsEx.complexion);

                    foreach (var item in rescom.data.documentos)
                    {
                        if (item.id_complexion == idRegister)
                        {
                            respuesta.CODIGO = item.id_complexion;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }
                    return respuesta;

                case ConstantsEx.SICM_TAMANIO_CABELLO:

                    mpResponse rescab = await getCatalogosID(ConstantsEx.longitud_cabello);

                    foreach (var item in rescab.data.documentos)
                    {
                        if (item.id_longitud_cabello == idRegister)
                        {
                            respuesta.CODIGO = item.id_longitud_cabello;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }
                    return respuesta;

                case ConstantsEx.SICM_TEZ:

                    mpResponse restez = await getCatalogosID(ConstantsEx.tipo_color_piel);

                    foreach (var item in restez.data.documentos)
                    {
                        if (item.id_tipo_color_piel == idRegister)
                        {
                            respuesta.CODIGO = item.id_tipo_color_piel;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }
                    return respuesta;

                case ConstantsEx.SICM_TIPO_CABELLO:

                    mpResponse restcab = await getCatalogosID(ConstantsEx.tipo_cabello);

                    foreach (var item in restcab.data.documentos)
                    {
                        if (item.id_tipo_cabello == idRegister)
                        {
                            respuesta.CODIGO = item.id_tipo_cabello;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }  
                    return respuesta;

                case ConstantsEx.SICM_TIPO_CEJA:

                    mpResponse resceja = await getCatalogosID(ConstantsEx.tipo_ceja);

                    foreach (var item in resceja.data.documentos)
                    {
                        if (item.id_tipo_ceja == idRegister)
                        {
                            respuesta.CODIGO = item.id_tipo_ceja;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }
                    return respuesta;

                case ConstantsEx.SICM_TIPO_NARIZ:
                    mpResponse resnariz = await getCatalogosID(ConstantsEx.tipo_nariz);

                    foreach (var item in resnariz.data.documentos)
                    {
                        if (item.id_tipo_nariz == idRegister)
                        {
                            respuesta.CODIGO = item.id_tipo_nariz;
                            respuesta.NOMBRE = item.titulo;
                        }
                    }
                    return respuesta;

                default:
                    respuesta.CODIGO = 1000;
                    return respuesta;
            }

        }
   
        public class mpResponse
        {
            public data data { get; set; }
        }

        public class data
        {
            public List<dataCatalogos> documentos { get; set; }
        }

        public class dataCatalogos
        {
            public int id_tipo_color_piel { get; set; }
            public int id_complexion { get; set; }
            public int id_tipo_nariz { get; set; }
            public int id_tipo_ceja { get; set; }
            public int id_tipo_ojo { get; set; }
            public int id_color_cabello { get; set; }
            public int id_longitud_cabello { get; set; }
            public int id_tipo_cabello { get; set; }
            public string titulo { get; set; }
        }
        public class newCatalogo
        {
            public int CODIGO { get; set; }
            public string NOMBRE { get; set; }
        }
    }
}
