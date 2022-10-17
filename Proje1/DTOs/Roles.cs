using System.ComponentModel.DataAnnotations;

namespace Proje1.DTOs
{
    public enum Roles
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "User")]
        User = 2,
    }
}
