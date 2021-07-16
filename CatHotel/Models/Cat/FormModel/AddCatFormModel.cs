namespace CatHotel.Models.Cat.FormModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ViewModel;
    using static Data.DataConstants.Cat;

    public class AddCatFormModel
    {
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; init; }

        [Range(MinAge, MaxAge, ErrorMessage = "Age can be between 1 and 20 years.")]
        public int Age { get; init; }

        [Url(ErrorMessage = "Please provide a valid URL.")]
        [DisplayName("Photo Url")]
        public string PhotoUrl { get; init; }

        [Display(Name = "Breed")]
        public int BreedId { get; init; }

        [StringLength(Int32.MaxValue, MinimumLength = MinAdditionalInformation, ErrorMessage = "Description can't be less then 10 characters long.")]
        [Display(Name = "Additional Information")]
        public string AdditionalInformation { get; set; }

        public IEnumerable<CatBreedViewModel> Breeds { get; set; }
    }
}