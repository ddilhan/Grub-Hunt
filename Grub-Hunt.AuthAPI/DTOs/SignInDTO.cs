namespace Grub_Hunt.AuthAPI.DTOs
{
    public class SignInDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
