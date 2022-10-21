namespace Organization.WebApi.DTOs
{
    public class ActivitiesDto
    {

        public int ActivityID { get; set; }
        public string Name { get; set; }
        public string ActivityDate { get; set; }
        public string ClosedDate { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public bool IsTicked { get; set; }
        public string CategoryName { get; set; }

    }
}
