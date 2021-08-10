namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CatReservation
    {
        [Key]
        public string CatId { get; set; }

        public Cat Cat { get; set; }

        [Key]
        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}