using Microsoft.AspNetCore.Identity;

namespace Grub_Hunt.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int FirstName { get; set; }
        public int LastName { get; set; }
    }
}
