namespace Grub_Hunt.AuthAPI.Models
{
    public class TokenProperties
    {
        public ApplicationUser user { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; }
    }
}
