namespace Organization.App.DTOs
{
    public class TicketActivityDto
    {

        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public int SelectedCompany { get; set; }
        public int UserID { get; set; }
     
        public List<TicketCompanyDto> TicketCompanies { get; set; }

    }

    public class TicketCompanyDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        //public string DomainName { get; set; } = null!;

        public string? Token { get; set; }
    }
}
