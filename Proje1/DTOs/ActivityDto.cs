namespace Proje1.DTOs
{
    public class ActivityDto
    {
        public string Name { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.Now;
        public DateTime ClosedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public List<CityDto>? Cities { get; set; }
        public int? SelectedCity { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public bool IsTicked { get; set; }
        public List<CategoryDto>? Categories { get; set; }
        public int? SelectedCategory { get; set; }

    }
}
