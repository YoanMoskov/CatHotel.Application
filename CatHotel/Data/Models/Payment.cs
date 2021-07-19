namespace CatHotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Payment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Column(TypeName="MONEY")]
        public decimal TotalPrice { get; set; }

        [Required]
        public bool isPaid { get; set; }
    }
}