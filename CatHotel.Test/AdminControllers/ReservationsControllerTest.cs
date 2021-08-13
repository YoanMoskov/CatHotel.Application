namespace CatHotel.Test.AdminControllers
{
    using Areas.Admin.Controllers;
    using Areas.Admin.Models.Reservations;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.Reservations;
    using static WebConstants;

    public class ReservationsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithModel()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.All(new AdminAllReservationsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllReservationsQueryModel>());

        [Fact]
        public void ApproveShouldRedirectToAction()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.Approve(TestReservation.Id))
                .ShouldReturn()
                .RedirectToAction("All");
    }
}