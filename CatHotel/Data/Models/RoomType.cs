namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Room;

    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }

        [Required]
        [MaxLength(MaxTypeNameLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerDay { get; set; }
    }
}