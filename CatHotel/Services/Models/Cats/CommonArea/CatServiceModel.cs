namespace CatHotel.Services.Models.Cats.CommonArea
{
    using System.ComponentModel;

    public class CatServiceModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public int Age { get; init; }

        [DisplayName("Photo Url")]
        public string PhotoUrl { get; init; }

        public string BreedName { get; init; }

        public int BreedId { get; init; }

        [DisplayName("Additional Information")]
        public string AdditionalInformation { get; init; }
    }
}