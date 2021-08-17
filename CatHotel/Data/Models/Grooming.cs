namespace CatHotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Grooming
    {
        [Key] 
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required] 
        public int StyleId { get; set; }

        public Style Style { get; set; }

        public DateTime Appointment { get; set; }

        public ICollection<CatGrooming> CatsGroomings { get; set; } = new List<CatGrooming>();
    }
}