namespace CatHotel.Areas.Admin.Models
{
    using Services.Models.Cats.AdminArea;
    using System.Collections.Generic;
    using Enums;
    using Services.Models.Cats.CommonArea;

    public class AdminAllCatsQueryModel
    {
        public const int CatsPerPage = 6;

        public string Breed { get; init; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCats { get; set; }

        public CatSorting Sorting { get; init; }

        public CatFiltering Filtering { get; init; }

        public IEnumerable<CatBreedServiceModel> Breeds { get; set; }

        public IEnumerable<AdminCatServiceModel> Cats { get; set; }
    }
}