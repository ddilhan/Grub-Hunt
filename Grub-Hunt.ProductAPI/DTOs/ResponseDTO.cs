using System.Net;

namespace Grub_Hunt.ProductAPI.DTOs
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public string? Token { get; set; } = null;
        public string Message { get; set; } = "";
        public bool Success { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; }
    }
}
