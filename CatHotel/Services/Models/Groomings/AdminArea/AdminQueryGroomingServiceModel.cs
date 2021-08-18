using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatHotel.Services.Models.Groomings.AdminArea
{
    using CommonArea;

    public class AdminQueryGroomingServiceModel
    {
        public int CurrentPage { get; set; }

        public int GroomsPerPage { get; set; }

        public int TotalGroomings { get; set; }

        public IEnumerable<GroomingServiceModel> Groomings { get; set; }
    }
}
