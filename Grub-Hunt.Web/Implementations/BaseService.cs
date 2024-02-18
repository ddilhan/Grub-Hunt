using Grub_Hunt.AuthAPI.DTOs;
using Grub_Hunt.Web.DTOs;
using System.Text;
using System.Text.Json;
using System.Net;
using Grub_Hunt.Web.Interfaces;

namespace Grub_Hunt.Web.Implementations
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                // token

                message.Method = request.HttpMethod;
                message.RequestUri = new Uri(request.Url);

                if (request.HttpMethod != HttpMethod.Get)
                {
                    message.Content = new StringContent(JsonSerializer.Serialize(request.Data)
                        , Encoding.UTF8, "application/json");
                }

                var apiResponse = await client.SendAsync(message);
                ResponseDTO? response = new();

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        response = JsonSerializer.Deserialize<ResponseDTO>(await apiResponse.Content.ReadAsStringAsync()
                            , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        response.StatusCode = apiResponse.StatusCode;
                        break;
                    case HttpStatusCode.BadRequest:
                        response = new() { Message = "Bad Request", StatusCode = apiResponse.StatusCode };
                        break;
                    case HttpStatusCode.Unauthorized:
                        response = new() { Message = "Unauthorized", StatusCode = apiResponse.StatusCode };
                        break;
                    case HttpStatusCode.InternalServerError:
                        response = new() { Message = "Internal Server Error", StatusCode = apiResponse.StatusCode };
                        break;
                }

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseDTO()
                {
                    Message = ex.Message, StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
