using External.ServiceApp.DTOs;
using External.ServiceApp.Services.ServicesContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace External.ServiceApp.Services
{
    public class MPService : IMPService
    {
        #region Consultar y descencripcion del token MP
        public async Task<tokenvalid> getTokenMP()
        {
            string url = GlobalsVariables.ConectionMPAuth;

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(values);

            tokenvalid isvalid = new tokenvalid();
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.DefaultRequestHeaders.Add("system", GlobalsVariables.SystemMPDesarrollo);
                    httpclient.DefaultRequestHeaders.Add($"Authorization", $"Basic {Base64Encode($"{GlobalsVariables.UsernameMP}:{GlobalsVariables.PasswordMP}")}");

                    var respuesta = httpclient.PostAsync(url, content).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string respuestastring = await respuesta.Content.ReadAsStringAsync();

                        res response = new res();
                        var resjson = JsonSerializer.Deserialize<res>(respuestastring,
                             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                        string decrypToken = Decrypt(resjson.token);

                        if (!String.IsNullOrEmpty(decrypToken))
                        {
                            isvalid = JsonSerializer.Deserialize<tokenvalid>(decrypToken,
                             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                            isvalid.isCorrect = true;
                        }
                        else
                        {
                            isvalid.isCorrect = false;
                            isvalid.accessToken = "No ha sido posible leer el token";
                        }                       
                    }
                    else
                    {
                        isvalid.isCorrect = false;
                        isvalid.accessToken = "Fallo la conexión";
                    }
                    return isvalid;
                }
            }
            catch (WebException)
            {
                isvalid.isCorrect = false;
                isvalid.accessToken = "Hubo error al crear la conexión con MP en la obtención del token";
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }
        public class res
        {
            public bool valid { get; set; }
            public string token { get; set; }
            public string respuesta { get; set; }
        }
        #endregion

        #region  Servicio para desencripción del token
        public class tokenvalid
        {
            public string accessToken { get; set; }
            public bool isCorrect { get; set; }
        }

        #region Descencriptacion de token
        public static string Decrypt(string cipherText)
        {
            string oktexto = OpenSSLDecrypt(cipherText, GlobalsVariables.KeyMP);
            return oktexto;
        }


        public static string OpenSSLDecrypt(string encrypted, string passphrase)
        {
            // base 64 decode
            byte[] encryptedBytesWithSalt = Convert.FromBase64String(encrypted);

            //decodificamos el salt de 8 bytes
            byte[] salt = new byte[8];
            byte[] encryptedBytes = new byte[encryptedBytesWithSalt.Length - salt.Length - 8];
            //parseamos el bloque de bytes
            Buffer.BlockCopy(encryptedBytesWithSalt, 8, salt, 0, salt.Length);
            Buffer.BlockCopy(encryptedBytesWithSalt, salt.Length + 8, encryptedBytes, 0, encryptedBytes.Length);

            byte[] key, iv;

            DeriveKeyAndIV(passphrase, salt, out key, out iv);
            //retornamos la descrencripcion por medio de bytes
            return DecryptStringFromBytesAes(encryptedBytes, key, iv);
        }

        private static void DeriveKeyAndIV(string passphrase, byte[] salt, out byte[] key, out byte[] iv)
        {
            List<byte> concatenatedHashes = new List<byte>(48);

            byte[] password = Encoding.UTF8.GetBytes(passphrase);
            byte[] currentHash = new byte[0];
            MD5 md5 = MD5.Create();
            bool enoughBytesForKey = false;
           
            while (!enoughBytesForKey)
            {
                int preHashLength = currentHash.Length + password.Length + salt.Length;
                byte[] preHash = new byte[preHashLength];

                Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
                Buffer.BlockCopy(password, 0, preHash, currentHash.Length, password.Length);
                Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + password.Length, salt.Length);

                currentHash = md5.ComputeHash(preHash);
                concatenatedHashes.AddRange(currentHash);

                if (concatenatedHashes.Count >= 48)
                    enoughBytesForKey = true;
            }

            key = new byte[32];
            iv = new byte[16];
            concatenatedHashes.CopyTo(0, key, 0, 32);
            concatenatedHashes.CopyTo(32, iv, 0, 16);

            md5.Clear();
        }


        static string DecryptStringFromBytesAes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            RijndaelManaged aesAlg = null;

            string plaintext;

            try
            {
                aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv };

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                 
                            plaintext = srDecrypt.ReadToEnd();
                            srDecrypt.Close();
                        }
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }
        #endregion

        #endregion

        #region servicio para consulta de archivos MP (boletin, oficio)
        public async Task<data> getFileMP(string idFile)
        {
            data data = new data();
            try
            {
                string url = GlobalsVariables.ConectionMPArchivo;

                tokenvalid respuesta = await getTokenMP();
                if (respuesta.isCorrect == true)
                {                 
                    using (var httpclient = new HttpClient())
                    {
                        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + idFile))
                        {
                            requestMessage.Headers.Add("system", "MINEX");
                            requestMessage.Headers.Add("cui", "13478798789");
                            requestMessage.Headers.Add("Authorization", "Bearer " + respuesta.accessToken);
                            var response = await httpclient.SendAsync(requestMessage);

                            if (response.IsSuccessStatusCode)
                            {
                                var contenido = await response.Content.ReadAsStringAsync();

                                data = JsonSerializer.Deserialize<data>(contenido,
                                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                                if(data.tipoRespuesta == "1")
                                {
                                    data.valid = true;
                                    //Aca ya se tiene el base 64 del archivo en ####### data.archivo  #######                                    
                                }
                                else //si la respuesta del mp en su campo valid es falso es porque el archivo no existe e su icloud
                                {
                                    data.message = "No se encontró archivo buscado con ID: " + idFile;
                                   
                                }
                            }
                            else
                            {
                                data.valid = false;
                                data.message = "Fallo la conexión";
                            }
                        }
                    }
                }
                else
                {
                    data.message = respuesta.accessToken;
                    data.valid = false;
                }
                return data;
            }
            catch (WebException wex)
            {
                data.message =  wex.Message;
                data.archivo = "Hubo error al crear la conexión con MP al obtener boletín";
                data.valid = false;
                return data;
            }
            catch (Exception ex)
           {
                throw ex;
            }
        }

        public class data
        {
            public string tipoRespuesta { get; set; }
            public string archivo { get; set; }
            public string idFile { get; set; }
            public string message { get; set; }
            public bool valid { get; set; }
        }
        #endregion
    }
}
