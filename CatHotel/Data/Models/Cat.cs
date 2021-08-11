namespace CatHotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using static DataConstants.Cat;

    public class Cat
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime DateAdded { get; set; }

        public string AdditionalInformation { get; set; }

        public CatSize CatSize { get; set; }

        [Required]
        public int BreedId { get; set; }

        public Breed Breed { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public bool IsDeleted { get; set; }

        public string GroomingId { get; set; }

        public Grooming Grooming { get; set; }

        public ICollection<CatReservation> CatsReservations { get; set; } = new List<CatReservation>();
    }
}