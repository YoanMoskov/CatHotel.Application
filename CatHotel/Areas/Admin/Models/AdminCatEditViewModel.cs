namespace CatHotel.Areas.Admin.Models
{
    using Services.Models.Cats.CommonArea;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class AdminCatEditViewModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        [DisplayName("Photo URL")]
        public string PhotoUrl { get; set; }

        [DisplayName("Breed")]
        public int BreedId { get; set; }

        [DisplayName("Additional Information")]
        public string AdditionalInformation { get; set; }


        public IEnumerable<CatBreedServiceModel> Breeds { get; set; }
    }
}