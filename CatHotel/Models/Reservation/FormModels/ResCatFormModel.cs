namespace CatHotel.Models.Reservation.FormModels
{
    using Services.Models.Cats.CommonArea;

    public class ResCatFormModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CatBreedServiceModel Breed { get; set; }
    }
}