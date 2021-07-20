namespace CatHotel.Data.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants.User;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string FirstName { get; init; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string LastName { get; init; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime BirthDate { get; init; }

        public ICollection<Cat> Cats { get; } = new List<Cat>();

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}