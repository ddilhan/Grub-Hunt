using System.Net;

namespace Grub_Hunt.AuthAPI.DTOs
{
    public class RequestDTO
    {
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;
        public string? Url { get; set; }
        public object? Data { get; set; }
        public string? Token { get; set; }
    }
}
