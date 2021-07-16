namespace CatHotel.Models.Reservation.FormModels
{
    using Cat;
    using Cat.ViewModel;

    public class ResCatFormModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CatBreedViewModel Breed { get; set; }
    }
}
