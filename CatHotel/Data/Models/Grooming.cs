namespace CatHotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Grooming
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required] public string UserId { get; set; }

        public User User { get; set; }

        public string CatId { get; set; }

        public Cat Cat { get; set; }

        [Required] public int StyleId { get; set; }

        public Style Style { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime Appointment { get; set; }

        public bool IsApproved { get; set; }

        public bool IsExpired { get; set; }
    }
}