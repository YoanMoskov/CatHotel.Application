namespace CatHotel.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.User;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string LastName { get; set; }

        [Required]
        public string AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<Cat> Cats { get; set; } = new List<Cat>();

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}