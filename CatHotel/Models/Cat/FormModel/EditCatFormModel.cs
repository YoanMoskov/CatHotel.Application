namespace CatHotel.Models.Cat.FormModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Cat;

    public class EditCatFormModel
    {
        [Range(MinAge, MaxAge, ErrorMessage = "Age can be between 1 and 20 years.")]
        public int Age { get; init; }

        [Url(ErrorMessage = "Please provide a valid URL.")]
        [DisplayName("Photo Url")]
        public string PhotoUrl { get; init; }

        [StringLength(Int32.MaxValue, MinimumLength = MinAdditionalInformation, ErrorMessage = "Additional Information can't be less then 10 characters long.")]
        [DisplayName("Additional Information")]
        public string AdditionalInformation { get; set; }
    }
}