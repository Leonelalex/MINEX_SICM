using System;
using System.IO;

namespace External.ServiceApp.Services
{
    public class pruebaArchivo
    {
        public static async void getarchivo(dataFile archivo)
        {
            byte[] imgByte = Convert.FromBase64String(archivo.archivo);
           // String path = HttpContext.Current.Server.MapPath("C:/SICM_DOCS/IC");
            string path = "C:/SICM_DOCS/IC";

            try
            {
                //darle nombre al archivo
                string fileName = "archivoprueba" ;
                string paht = Path.Combine(path, fileName); 

                await File.WriteAllBytesAsync(paht, imgByte);           

            }
            catch (Exception ex)
            {
                var errorMsg = ex.ToString();
            }
        }

        public class dataFile
        {
            public string archivo { get; set; }
        }
    }
}
