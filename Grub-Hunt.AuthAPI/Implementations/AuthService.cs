using Grub_Hunt.AuthAPI.DTOs;
using Grub_Hunt.AuthAPI.Interfaces;
using Grub_Hunt.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Grub_Hunt.AuthAPI.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> SignUp(SignUpDTO model)
        {
            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return await _userManager.FindByEmailAsync(user.Email);
            else
                return null;
        }

        public async Task<ApplicationUser?> SignIn(SignInDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var status = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user is null || status is false)
                return null;
            else
                return user;
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AssignRole(string? email, string? role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null) 
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                var ret = await _userManager.AddToRoleAsync(user, role);
                return ret.Succeeded;
            }
            else
                return false;
        }
    }
}
