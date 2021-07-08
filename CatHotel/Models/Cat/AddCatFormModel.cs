namespace CatHotel.Models.Cat
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Cat;

    public class AddCatFormModel
    {
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; init; }

        [Range(MinAge, MaxAge, ErrorMessage = "Age can be between 1 and 20 years.")]
        public int Age { get; init; }

        [Url(ErrorMessage = "Please provide a valid URL.")]
        [DisplayName("Picture Url")]
        public string PhotoUrl { get; init; }

        [Display(Name = "Breed")]
        public int BreedId { get; init; }

        public IEnumerable<CatBreedViewModel> Breeds { get; set; }
    }
}