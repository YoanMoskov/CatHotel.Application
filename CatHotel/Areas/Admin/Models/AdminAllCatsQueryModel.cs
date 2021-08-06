namespace CatHotel.Areas.Admin.Models
{
    using Services.Models.Cats.AdminArea;
    using System.Collections.Generic;

    public class AdminAllCatsQueryModel
    {
        public int CatsPerPage = 5;

        public int CurrentPage { get; set; }

        public int TotalCats { get; set; }

        public IEnumerable<string> Breeds { get; set; }

        public IEnumerable<AdminCatServiceModel> Cats { get; set; }
    }
}