namespace CatHotel.Models.Cat.FormModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Services.Models.Cats.CommonArea;
    using static Data.DataConstants.Cat;

    public class AddCatFormModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; init; }

        [Required]
        [Range(MinAge, MaxAge, ErrorMessage = "Age can be between 1 and 20 years.")]
        public int Age { get; init; }

        [Required]
        [Url(ErrorMessage = "Please provide a valid URL.")]
        [DisplayName("Photo Url")]
        public string PhotoUrl { get; init; }

        [Required]
        [Display(Name = "Breed")]
        public int BreedId { get; init; }

        [StringLength(Int32.MaxValue, MinimumLength = MinAdditionalInformation, ErrorMessage = "Description can't be less then 10 characters long.")]
        [Display(Name = "Additional Information")]
        public string AdditionalInformation { get; init; }

        public IEnumerable<CatBreedServiceModel> Breeds { get; set; }
    }
}