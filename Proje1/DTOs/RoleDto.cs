namespace Proje1.DTOs
{
    public class RoleDto
    {

        public int RoleId { get; set; }
        public string Role1 { get; set; } = null!;

        public virtual ICollection<UserDto> Users { get; set; }
    }
}
