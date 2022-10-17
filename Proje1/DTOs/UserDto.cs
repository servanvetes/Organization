
using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Proje1.DTOs
{
    public class UserDto : User
    {

        public int? UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string RePassword { get; set; } = null!;
        public bool IsRememberMe { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int RoleId { get; set; } = (int)Roles.User;

        public virtual RoleDto? Role { get; set; }
        // public virtual ICollection<Activity> Activities { get; set; }
    }
}
