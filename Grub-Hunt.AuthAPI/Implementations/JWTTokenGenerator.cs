using Grub_Hunt.AuthAPI.Interfaces;
using Grub_Hunt.AuthAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Grub_Hunt.AuthAPI.Implementations
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        private readonly JWTOptions _jWTOptions;
        public JWTTokenGenerator(IOptions<JWTOptions> jWTOptions)
        {
            _jWTOptions = jWTOptions.Value;
        }
        public string GenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTOptions?.Secret);

            var claimList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user?.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _jWTOptions?.Audience,
                Issuer = _jWTOptions?.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
