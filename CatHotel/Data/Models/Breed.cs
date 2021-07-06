namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Breed;

    public class Breed
    {
        [Key]
        public int BreedId { get; set; }

        [Required]
        [MaxLength(MaxBreedLength)]
        public string Name { get; set; }
    }
}