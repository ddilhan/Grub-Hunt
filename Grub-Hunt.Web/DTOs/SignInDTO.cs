using System.ComponentModel.DataAnnotations;

namespace Grub_Hunt.Web.DTOs
{
    public class SignInDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        //password complexity
        public string? Password { get; set; }
    }
}
