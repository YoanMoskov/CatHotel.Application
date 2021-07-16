namespace CatHotel.Models.Cat.ViewModel
{
    using System.ComponentModel;

    public class CatViewModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public int Age { get; init; }

        [DisplayName("Photo Url")]
        public string PhotoUrl { get; init; }

        [DisplayName("Additional Information")]
        public string AdditionalInformation { get; set; }

        public CatBreedViewModel Breed { get; init; }
    }
}