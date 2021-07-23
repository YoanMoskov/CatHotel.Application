namespace CatHotel.Models.RoomType
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Data.DataConstants.RoomType;

    public class ResRoomTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTypeNameLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public decimal PricePerDay { get; set; }
    }
}