namespace CatHotel.Models.Reservation
{
    using Cat;

    public class ResCatViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CatBreedViewModel Breed { get; set; }
    }
}
