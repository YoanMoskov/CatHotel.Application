namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Breed;

    public class Breed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxBreedLength)]
        public string Name { get; set; }
    }
}