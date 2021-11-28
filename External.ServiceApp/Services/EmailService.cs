using Core.ServiceApp.Helpers;
using External.ServiceApp.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace External.ServiceApp.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> sendEmailAK(EmailModel model)
        {
            string url = GlobalsVariables.EnviromentConnApiEmailAttach;
            string filePath = GlobalsVariables.AKPics_Path;
            string oficioPath = GlobalsVariables.AKOficios_Path;
            string template = getAKTemplate();
            model.listDocs = model.listDocs.Trim();
            template = template.Replace("[codAlerta]", model.numeroAlerta);
            template = template.Replace("[titulo]", model.titulo);
            template = template.Replace("[saludo]", model.saludo);
            template = template.Replace("[datos]", model.datosAlerta);
            template = template.Replace("[fin]", "");
            template = template.Replace("[contenido]", model.body);

            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        string correosMisiones = getCorreosMisiones(model.codAlerta);
                        content.Add(new StringContent(correosMisiones + model.listCorreos), "To");
                        content.Add(new StringContent(model.subjet), "Subject");
                        content.Add(new StringContent(template), "Body");

                        if (model.oficio != null && model.oficio != "")
                        {
                            var oficioDoc = new ByteArrayContent(File.ReadAllBytes(oficioPath + model.oficio));
                            oficioDoc.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = model.oficio + "file",
                                FileName = model.oficio
                            };
                            oficioDoc.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            content.Add(oficioDoc);
                        }

                        if (model.listDocs != null && model.listDocs != "")
                        {
                            List<string> docs = new List<string>();
                            if (model.listDocs.Contains(","))
                                docs = model.listDocs.Split(",").ToList();
                            else
                                docs.Add(model.listDocs);

                            foreach (string doc in docs)
                            {
                                var filecontent = new ByteArrayContent(File.ReadAllBytes(filePath + doc));
                                filecontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = doc + "file",
                                    FileName = doc
                                };
                                filecontent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                                content.Add(filecontent);
                            }
                        }

                        var response = await client.PostAsync(url, content);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> sendActionEmailAK(EmailModel model)
        {
            string url = GlobalsVariables.EnviromentConnApiEmailAttach; 
            string filePath = GlobalsVariables.AKDocs_Path;
            string template = getAKTemplate();
            model.listDocs = model.listDocs.Trim();
            template = template.Replace("[codAlerta]", model.numeroAlerta);
            template = template.Replace("[titulo]", model.titulo);
            template = template.Replace("[saludo]", model.saludo);
            template = template.Replace("[datos]", model.datosAlerta);
            template = template.Replace("[fin]", "");
            template = template.Replace("[contenido]", model.body);

            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {

                        string correosMisiones = getCorreosMisiones(model.codAlerta);
                        content.Add(new StringContent(correosMisiones + model.listCorreos), "To");
                        content.Add(new StringContent(model.subjet), "Subject");
                        content.Add(new StringContent(template), "Body");


                        if (model.listDocs != null && model.listDocs != "")
                        {
                            List<string> docs = new List<string>();
                            if (model.listDocs.Contains(","))
                                docs = model.listDocs.Split(",").ToList();
                            else
                                docs.Add(model.listDocs);

                            foreach (string doc in docs)
                            {
                                var filecontent = new ByteArrayContent(File.ReadAllBytes(filePath + doc));
                                filecontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = doc + "file",
                                    FileName = doc
                                };
                                filecontent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                                content.Add(filecontent);
                            }
                        }

                        var response = await client.PostAsync(url, content);
                        
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> sendEmailIC(EmailModel model)
        {
            string url = GlobalsVariables.EnviromentConnApiEmailAttach;

            string filePath = GlobalsVariables.ICPics_Path;
            string oficioPath = GlobalsVariables.ICOficios_Path;

            string template = getICTemplate();
            template = template.Replace("[titulo]", model.titulo);
            template = template.Replace("[saludo]", model.saludo);
            template = template.Replace("[datos]", model.datosAlerta);
            template = template.Replace("[fin]", "");
            template = template.Replace("[contenido]", model.body);       

            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        string correosMisiones = getCorreosMisiones(model.codAlerta);
                        content.Add(new StringContent(model.listCorreos), "To");
                        content.Add(new StringContent(model.subjet), "Subject");
                        content.Add(new StringContent(template), "Body");

                        if (!String.IsNullOrEmpty(model.oficio))
                        {
                            var oficioDoc = new ByteArrayContent(File.ReadAllBytes(oficioPath + model.oficio));
                            oficioDoc.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = model.oficio + "file",
                                FileName = model.oficio
                            };
                            oficioDoc.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            content.Add(oficioDoc);
                        }

                        if (!String.IsNullOrEmpty(model.boletinIC))
                        {
                            var filecontent = new ByteArrayContent(File.ReadAllBytes(filePath + model.boletinIC));
                            filecontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = model.boletinIC + "file",
                                FileName = model.boletinIC
                            };
                            filecontent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                            content.Add(filecontent);
                        }

                        var response = await client.PostAsync(url, content);

                        if(response.IsSuccessStatusCode)
                        {
                            return true;

                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> sendActionEmailIC(EmailModel model)
        {
            string url = GlobalsVariables.EnviromentConnApiEmailAttach;

            string oficioPath = GlobalsVariables.ICDocs_Path;
            string template = getICTemplate();
            template = template.Replace("[titulo]", model.titulo);
            template = template.Replace("[saludo]", model.saludo);
            template = template.Replace("[datos]", model.datosAlerta);
            template = template.Replace("[fin]", "");
            template = template.Replace("[contenido]", model.body);

            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        string correosMisiones = getCorreosMisiones(model.codAlerta);
                        content.Add(new StringContent(correosMisiones + model.listCorreos), "To");
                        content.Add(new StringContent(model.subjet), "Subject");
                        content.Add(new StringContent(template), "Body");

                        if (model.oficio != null && model.oficio != "")
                        {
                            var oficioDoc = new ByteArrayContent(File.ReadAllBytes(oficioPath + model.oficio));
                            oficioDoc.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = model.oficio + "file",
                                FileName = model.oficio
                            };
                            oficioDoc.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            content.Add(oficioDoc);
                        }

                        var response = await client.PostAsync(url, content);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> sendEmaiErrorlIC(EmailModel model)
        {
            string url = GlobalsVariables.EnviromentConnApiEmailAttach;

            string filePath = GlobalsVariables.ICPics_Path;
            string oficioPath = GlobalsVariables.ICOficios_Path;

            string template = getICTemplate();
            template = template.Replace("[titulo]", model.titulo);
            template = template.Replace("[saludo]", model.saludo);
            template = template.Replace("[datos]", model.datosAlerta);
            template = template.Replace("[fin]", "");
            template = template.Replace("[contenido]", model.body);

            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        string correosMisiones = getCorreosMisiones(model.codAlerta);
                        content.Add(new StringContent(correosMisiones + model.listCorreos), "To");
                        content.Add(new StringContent(model.subjet), "Subject");
                        content.Add(new StringContent(template), "Body");

                        var response = await client.PostAsync(url, content);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getCorreosMisiones(int codAlerta)
        {
            return "";
        }

        public string getAKTemplate()
        {
            //string filePath = "../Core.ServiceApp/Email_Templates/AK_Template.html";
            string filePath = GlobalsVariables.RouteTemplateAK;
            string file = File.ReadAllText(filePath);

            return file;
        }

        public string getICTemplate()
        {
            string filePath = GlobalsVariables.RouteTemplateIC;
            string file = File.ReadAllText(filePath);
            return file;
        }
    }
}