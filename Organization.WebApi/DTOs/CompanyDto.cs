namespace Organization.WebApi.DTOs
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string DomainName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Token { get; set; }
    }
}
