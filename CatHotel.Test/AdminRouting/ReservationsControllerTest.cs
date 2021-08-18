namespace CatHotel.Test.AdminRouting
{
    using Areas.Admin.Controllers;
    using Areas.Admin.Models.Reservations;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Reservations;
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
                .To<ReservationsController>(c => c.All(new AdminAllReservationsQueryModel()));

        [Fact]
        public void ApproveRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithLocation("/Admin/Reservations/Approve?resId=1"))
                .To<ReservationsController>(c => c.Approve(TestReservation.Id));
    }
}