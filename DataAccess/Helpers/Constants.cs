namespace DataAccess.Helpers
{
    public class Constants
    {
        #region HTTP Codes

        public const int OK_code = 200;
        public const int NoContent_code = 204;
        public const int BadRequest_code = 400;
        public const int NoAutorized_code = 403;
        public const int SessionExpired_code = 401;
        public const int Error_code = 500;

        #endregion

        #region Messages

        public const string BadRequest_msj = "Bad Request";
        public const string NoAutorized_msj = "No autorizado";
        public const string SessionExpired_msj = "Sesion expirada";

        #endregion

        #region log constants

        public const string level_INFO = "INFO";
        public const string level_WARN = "WARN";
        public const string level_ERROR = "ERROR";

        #endregion

        #region Files constants

        public const string Oficio = "oficio";

        #endregion

        //sesion
        public const string SessionToken = "SessionToken";

        //Institutions
        public static string InstitutionMinex = "Ministerio de Relaciones Exteriores";

        //code Status Alert
        public const int CodeRegisterAlert = 1;
        public const int CodeActiveAlert = 2;
        public const int CodeDesactiveAlert = 3;

        //Code sex
        public static int CodeMan = 1;
        public static int CodeFemale = 2;

        //Code Situations
        public static int SinEspecificar = 1;

        //Code Type Alert
        public static int AlbaKeneth = 1;
        public static int IsabelClaudina = 2;

        //Message
        public const string BadModelState = "Campos Incorrectos";
        public const string NoCreateAlert = "No se pudo crear la alerta";
        public const string CreateAlertOk = "Alerta creada Exitosamente";
        public const string UpdateAlertOk = "Alerta actualizada Exitosamente";
        public const string NoUpdateAlert = "No se pudo desactivar la alerta";
    }
}
