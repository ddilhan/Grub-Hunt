using Grub_Hunt.AuthAPI.DTOs;
using Grub_Hunt.Web.DTOs;

namespace Grub_Hunt.Web.Interfaces
{
    public interface IBaseService
    {
        public Task<ResponseDTO?> SendAsync(RequestDTO request, bool withToken = true);
    }
}
