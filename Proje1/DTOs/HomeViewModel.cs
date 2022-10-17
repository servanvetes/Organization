namespace Proje1.DTOs
{
    public class HomeViewModel
    {
        public List<Sehir> Sehirler { get; set; }
        public int SelectedSehir { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.Now;
        public DateTime ClosedDate { get; set; } = DateTime.Now;
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityAdres { get; set; }

        public int Limit { get; set; }

        public bool IsTicked { get; set; }


    }



    public class Sehir
    {
        public int Id { get; set; }
        public string Ad { get; set; }
    }
}
