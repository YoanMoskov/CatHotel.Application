namespace CatHotel.Controllers.ViewModels.Cat
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Cat;

    public class CatFormModel
    {
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [Range(MinAge, MaxAge, ErrorMessage = "Age can be between 1 and 20 years.")]
        public int Age { get; set; }

        [Url(ErrorMessage = "Please provide a valid URL.")]
        [DisplayName("Picture Url")]
        public string PhotoUrl { get; set; }

        [DisplayName("Specify breed")]
        public int BreedId { get; set; }
    }
}