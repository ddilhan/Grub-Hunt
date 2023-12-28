using Grub_Hunt.AuthAPI.DTOs;
using Grub_Hunt.AuthAPI.Models;

namespace Grub_Hunt.AuthAPI.Interfaces
{
    public interface IAuthService
    {
        public Task<ApplicationUser?> SignUp(SignUpDTO model);
        public Task<ApplicationUser?> SignIn(SignInDTO model);
        public void SignOut();
        public Task<bool> AssignRole(string? email, string? role);
    }
}
