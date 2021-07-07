namespace CatHotel.Controllers.ViewModels.Cat
{
    using System.ComponentModel;

    public class CatViewModel
    {
        public string CatId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        [DisplayName("Photo Url")]
        public string PhotoUrl { get; set; }

        public CatBreedViewModel Breed { get; set; }
    }
}