namespace CatHotel.Areas.Admin.Models.Groomings
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums.Groomings;
    using Services.Models.Groomings.CommonArea;

    public class AdminAllGroomingsQueryModel
    {
        public const int GroomsPerPage = 6;

        [Display(Name = "Style")]
        public string StyleId { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalGroomings { get; set; }

        public GroomsSorting Sorting { get; set; }

        public GroomsFiltering Filtering { get; set; }

        public IEnumerable<GroomingStyleServiceModel> Styles { get; set; }

        public IEnumerable<GroomingServiceModel> Grooms { get; set; }
    }
}