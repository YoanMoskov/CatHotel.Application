namespace CatHotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Cat;

    public class Cat
    {
        [Key]
        public string CatId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public int BreedId { get; set; }

        public Breed Breed { get; set; }
    }
}