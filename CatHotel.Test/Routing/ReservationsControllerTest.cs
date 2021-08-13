namespace CatHotel.Test.Routing
{
    using CatHotel.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ReservationsControllerTest
    {
        [Fact]
        public void CreateRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("Reservations/Create")
                .To<ReservationsController>(c => c.Create());

        [Fact]
        public void PostCreateRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Reservations/Create")
                    .WithMethod(HttpMethod.Post))
                .To<ReservationsController>(c => c.Create());

        [Fact]
        public void ActiveRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Reservations/Active")
                .To<ReservationsController>(c => c.Active());

        [Fact]
        public void ApprovedRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Reservations/Approved")
                .To<ReservationsController>(c => c.Approved());

        [Fact]
        public void PendingApprovalRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Reservations/PendingApproval")
                .To<ReservationsController>(c => c.PendingApproval());
    }
}