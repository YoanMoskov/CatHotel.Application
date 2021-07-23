namespace CatHotel.Data.Models
{
    public class CatReservation
    {
        public string CatId { get; set; }

        public Cat Cat { get; set; }

        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}