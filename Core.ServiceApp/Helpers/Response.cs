namespace Core.ServiceApp.Helpers
{
    public class Response
    {
        public int codigo { get; set; } 
        public string message { get; set; } 
        public string innerError { get; set; }
        public object data { get; set; }
    }

}
