namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Style
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}