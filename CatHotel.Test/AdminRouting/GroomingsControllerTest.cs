namespace CatHotel.Test.AdminRouting
{
    using Areas.Admin.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Groomings;

    public class GroomingsControllerTest
    {
        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("Admin/Groomings/All")
                .To<GroomingsController>(c => c.All(null));

        [Fact]
        public void ApproveRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("Admin/Groomings/Approve?groomId=TestGroomId")
                .To<GroomingsController>(c => c.Approve(TestGrooming.Id));
    }
}