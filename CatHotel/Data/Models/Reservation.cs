namespace CatHotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Reservation
    {
        [Key]
        public string ReservationId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime DateOfReservation { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public string PaymentId { get; set; }

        public Payment Payment { get; set; }

        public ICollection<Cat> Cats { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}