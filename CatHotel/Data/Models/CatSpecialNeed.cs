namespace CatHotel.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CatSpecialNeed
    {
        [Key]
        public string CatId { get; set; }

        public Cat Cat { get; set; }

        [Key]
        public int SpecialNeedId { get; set; }

        public SpecialNeed SpecialNeed { get; set; }
    }
}