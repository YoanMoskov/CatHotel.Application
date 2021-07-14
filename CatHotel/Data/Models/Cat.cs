namespace CatHotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        public int BreedId { get; set; }

        public Breed Breed { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}