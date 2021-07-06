namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        public bool IsTaken { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }
    }
}