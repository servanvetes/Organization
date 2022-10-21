using System.ComponentModel.DataAnnotations;

namespace Proje1.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool IsRememberMe { get; set; }
    }
}
