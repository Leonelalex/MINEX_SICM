using External.ServiceApp.DTOs;
using External.ServiceApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace External.ServiceApp.Services
{
    public class SSOService
    {
        public async static Task<bool> IsValidToken(string token)
        {
            string url = GlobalsVariables.EnvirommentSSO_Service;
            string IdSystem = GlobalsVariables.SistemId;
            string CodigoPermiso = GlobalsVariables.CodigoPermiso;

            var obj = new { systemId = IdSystem, codigoPermiso = CodigoPermiso };

            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.DefaultRequestHeaders.Add("SessionToken", token);

                    var respuesta = await httpclient.PutAsJsonAsync($"{url}", obj);

                    if (!respuesta.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    else
                    {
                        respuesta.Headers.GetValues("SessionToken");
                        string respuestastring = await respuesta.Content.ReadAsStringAsync();
                        obj Convert = JsonConvert.DeserializeObject<obj>(respuestastring);
                        GlobalsVariables.UserID = Convert.UserId;
                        GlobalsVariables.UserName = Convert.UserName;
                        GlobalsVariables.Pais = Convert.Pais;
                        GlobalsVariables.newToken = respuesta.Headers.TryGetValues("SessionToken", out var values) ? values.FirstOrDefault() : null;
                        return true;
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

        public async static Task<SSOResponse> IsValidPermition(string token, string CodigoPermiso)
        {
            SSOResponse ssoRes = new SSOResponse();
            string url = GlobalsVariables.EnvirommentSSO_Service;
            string IdSystem = GlobalsVariables.SistemId;

            var obj = new { systemId = IdSystem, codigoPermiso = CodigoPermiso };

            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.DefaultRequestHeaders.Add("SessionToken", token);

                    var respuesta = await httpclient.PutAsJsonAsync($"{url}", obj);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string respuestastring = await respuesta.Content.ReadAsStringAsync();
                        obj Convert = JsonConvert.DeserializeObject<obj>(respuestastring);
                        GlobalsVariables.UserID = Convert.UserId;
                        GlobalsVariables.UserName = Convert.UserName;
                        GlobalsVariables.Pais = Convert.Pais;
                        ssoRes.newToken = respuesta.Headers.TryGetValues("SessionToken", out var values) ? values.FirstOrDefault() : null;
                        ssoRes.codigo = 200;

                    }
                    else
                    {
                        ssoRes.codigo = 500;
                    }

                    return ssoRes;

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
        public class obj
        {
            public string message { get; set; }
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Pais { get; set; }
        }

    }
}
