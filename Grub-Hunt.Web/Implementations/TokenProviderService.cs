using Grub_Hunt.Web.Interfaces;
using Grub_Hunt.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Grub_Hunt.Web.Implementations
{
    public class TokenProviderService : ITokenProviderService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProviderService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void SetToken(string token)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append(StaticDetails.TokenCookie, token);
        }

        public string? GetToken()
        {
            string? token;
            var hasToken = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(StaticDetails.TokenCookie, out token);

            return hasToken is true ? token : null;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(StaticDetails.TokenCookie);
        }

        public ClaimsPrincipal GetClaimsPrincipal(string token) 
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));

            return new ClaimsPrincipal(identity);
        }
    }
}
