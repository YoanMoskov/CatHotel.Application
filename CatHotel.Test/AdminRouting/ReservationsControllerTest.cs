namespace CatHotel.Test.AdminRouting
{
    using MyTested.AspNetCore.Mvc;
    using Areas.Admin.Controllers;
    using Xunit;

    using static Areas.Admin.AdminConstants;

    public class ReservationsControllerTest
    {
        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithPath("/Admin/Reservations/All"))
                .To<ReservationsController>(c => c.All(null));

        [Fact]
        public void ApproveRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithPath("/Admin/Reservations/Approve"))
                .To<ReservationsController>();
    }
}