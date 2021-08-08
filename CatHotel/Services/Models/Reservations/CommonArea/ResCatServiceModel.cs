namespace CatHotel.Services.Models.Reservations.CommonArea
{
    public class ResCatServiceModel
    {
        public string PhotoUrl { get; set; }

        public string Name { get; init; }

        public string BreedName { get; init; }

        public bool IsDeleted { get; set; }
    }
}