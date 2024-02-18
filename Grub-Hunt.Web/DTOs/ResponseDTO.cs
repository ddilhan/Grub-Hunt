using System.Net;

namespace Grub_Hunt.AuthAPI.DTOs
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public string? Token { get; set; } = null;
        public string Message { get; set; } = "";
        public HttpStatusCode StatusCode { get; set; }
    }
}
