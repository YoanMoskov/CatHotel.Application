namespace CatHotel.Services.Models.Cats.AdminArea
{
    using System.Collections.Generic;

    public class AdminCatQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int CatsPerPage { get; set; }

        public int TotalCats { get; set; }

        public IEnumerable<AdminCatServiceModel> Cats { get; set; }
    }
}