namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SpecialNeed
    {
        [Key]
        public int SpecialNeedId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}