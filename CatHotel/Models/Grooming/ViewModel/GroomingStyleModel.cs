namespace CatHotel.Models.Grooming.FormModel
{
    public class GroomingStyleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }
    }
}