using System.Security.Claims;

namespace Grub_Hunt.Web.Interfaces
{
    public interface ITokenProviderService
    {
        public void SetToken(string token);
        public string? GetToken();
        public void ClearToken();
        public ClaimsPrincipal GetClaimsPrincipal(string token);
    }
}
