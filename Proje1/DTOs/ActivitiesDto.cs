namespace Proje1.DTOs
{
    public class ActivitiesDto
    {
        public int ActivityID { get; set; }
        public string Name { get; set; }
        public DateOnly ActivityDate { get; set; } 
        public DateOnly ClosedDate { get; set; } 
        public string Description { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public string IsTicked { get; set; }
        public string CategoryName { get; set; }
    }
}
