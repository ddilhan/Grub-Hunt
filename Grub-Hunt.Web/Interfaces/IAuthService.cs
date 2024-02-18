using Grub_Hunt.AuthAPI.DTOs;
using Grub_Hunt.Web.DTOs;

namespace Grub_Hunt.Web.Interfaces
{
    public interface IAuthService
    {
        public Task<ResponseDTO?> SignUpAsync(SignUpDTO model);
        public Task<ResponseDTO?> SignInAsync(SignInDTO model);
        public Task<ResponseDTO?> AssignRoleAsync(SignUpDTO model);
        public Task SignInUser(string token);
        public Task SignOutUser();
    }
}
