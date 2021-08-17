namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CatGrooming
    {
        [Key] 
        public string CatId { get; set; }

        public Cat Cat { get; set; }

        [Key] 
        public string GroomingId { get; set; }

        public Grooming Grooming { get; set; }
    }
}