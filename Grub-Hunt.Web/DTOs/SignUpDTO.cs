using System.ComponentModel.DataAnnotations;

namespace Grub_Hunt.Web.DTOs
{
    public class SignUpDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
