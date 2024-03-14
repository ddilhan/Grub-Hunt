using Grub_Hunt.AuthAPI.Models;

namespace Grub_Hunt.AuthAPI.Interfaces
{
    public interface IJWTTokenGenerator
    {
        public string GenerateToken(TokenProperties properties);
    }
}
