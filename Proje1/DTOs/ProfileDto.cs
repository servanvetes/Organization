namespace Proje1.DTOs
{
    public class ProfileDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
