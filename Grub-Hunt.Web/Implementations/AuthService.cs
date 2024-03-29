﻿using Grub_Hunt.AuthAPI.DTOs;
using Grub_Hunt.Web.DTOs;
using Grub_Hunt.Web.Interfaces;
using Grub_Hunt.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace Grub_Hunt.Web.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly ITokenProviderService _tokenProviderService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ServiceUrls _serviceUrls;
        public AuthService(IBaseService baseService, ITokenProviderService tokenProviderService, IHttpContextAccessor contextAccessor,
            IOptions<ServiceUrls> options)
        {
            _baseService = baseService;
            _tokenProviderService = tokenProviderService;
            _contextAccessor = contextAccessor;
            _serviceUrls = options.Value;
        }
        public async Task<ResponseDTO?> SignUpAsync(SignUpDTO model)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                HttpMethod = HttpMethod.Post,
                Url = $"{_serviceUrls.AuthAPIBaseUrl}/api/Auth/SignUp",
                Data = model
            });
        }

        public async Task<ResponseDTO?> SignInAsync(SignInDTO model)
        {
            var result =  await _baseService.SendAsync(new RequestDTO
            {
                HttpMethod = HttpMethod.Post,
                Url = $"{_serviceUrls.AuthAPIBaseUrl}/api/Auth/SignIn",
                Data = model
            });

            if (result is not null && result.Token is not null)
                _tokenProviderService.SetToken(result.Token);

            return result;
        }

        public async Task<ResponseDTO?> AssignRoleAsync(SignUpDTO model)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                HttpMethod = HttpMethod.Post,
                Url = $"{_serviceUrls.AuthAPIBaseUrl}/api/Auth/AssignRole",
                Data = model
            });
        }

        public async Task SignInUser(string token)
        {
            var principal = _tokenProviderService.GetClaimsPrincipal(token);
            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public async Task SignOutUser()
        {
            await _contextAccessor.HttpContext.SignOutAsync();
            _tokenProviderService.ClearToken();
        }
    }
}
