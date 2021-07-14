namespace CatHotel.Models.Cat
{
    using System.ComponentModel;

    public class CatViewModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public int Age { get; init; }

        [DisplayName("Photo Url")]
        public string PhotoUrl { get; init; }

        public CatBreedViewModel Breed { get; init; }
    }
}