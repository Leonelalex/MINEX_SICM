using External.ServiceApp.Services;
using External.ServiceApp.Services.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InterfaceApi.Controllers
{

    [Route("test")]
    [ApiController]
    public class pruebaController : ControllerBase
    {

        private readonly IMPService _iMPService;


        public pruebaController(IMPService mPService)
        {
            _iMPService = mPService;
        }
        #region controlodares de prueba para desencriptacion de token mp
        [HttpGet]
        public async Task<object> getprueba()
        {
            try
            {
                MPService serv = new MPService();

                return await _iMPService.getTokenMP();
            }
            catch (Exception)
            {
                return "fallo la obtencion del token";
            }
            // string dec = Decrypt(encrip, "HF8MtA");//  prueba uno de desencripcion

        }

        [HttpGet]
        [Route("test2")]
        public async Task<object> getpruebacatalogos()
        {
            return "Prueba exitosa";
        }

        [HttpGet]
        [Route("consultcatalogos")]
        public async Task<object> getprueba4(string catalogo)
        {
            try
            {
              return await CatalogoService.getCatalogosID("scp_eca_mc_tipo_color_piel");
            }
            catch (Exception)
            {
                return "fallo con excepcion";
            }
        }
        #endregion

    }
}
