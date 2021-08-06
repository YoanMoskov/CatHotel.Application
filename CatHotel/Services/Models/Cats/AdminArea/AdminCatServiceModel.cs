namespace CatHotel.Services.Models.Cats.AdminArea
{
    public class AdminCatServiceModel
    {
        public string Id { get; init; }

        public string UserEmail { get; set; }

        public string Name { get; init; }

        public int Age { get; init; }

        public string PhotoUrl { get; init; }

        public string BreedName { get; init; }

        public bool IsDeleted { get; set; }

        public string AdditionalInformation { get; init; }
    }
}