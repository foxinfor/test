using System.Net;

namespace BLL.DTO.Responces
{
    public class ErrorResponce
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
        //public string? StackTrace { get; set; } клиент не должен знать 
    }
}
