using System.Net;

namespace Grub_Hunt.AuthAPI.DTOs
{
    public class RequestDTO
    {
        public object? Result { get; set; }
        public HttpStatusCode? statusCode { get; set; }
        public string Message { get; set; } = "";
    }
}
