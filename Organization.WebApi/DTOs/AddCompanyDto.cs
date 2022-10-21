namespace Organization.WebApi.DTOs
{
    public class AddCompanyDto
    {

        public string Name { get; set; } = null!;
        public string DomainName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
