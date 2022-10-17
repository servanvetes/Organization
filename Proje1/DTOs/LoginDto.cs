namespace Proje1.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
    }
}
