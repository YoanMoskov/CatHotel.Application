namespace CatHotel.Areas.Admin.Models.Enums.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public enum ResFiltering
    {
        Approved = 10,
        PendingApproval = 20,
        Active = 30,
        Expired = 40,
        Pending = 50
    }
}