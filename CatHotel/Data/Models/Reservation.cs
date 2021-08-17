namespace CatHotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    public class Reservation
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime DateOfReservation { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime Arrival { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime Departure { get; set; }

        public string PaymentId { get; set; }

        public Payment Payment { get; set; }

        public int RoomTypeId { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public RoomType RoomType { get; set; }

        public ReservationState ReservationState { get; set; }

        public bool IsApproved { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public ICollection<CatReservation> CatsReservations { get; set; } = new List<CatReservation>();
    }
}