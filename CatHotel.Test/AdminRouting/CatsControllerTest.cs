namespace CatHotel.Test.AdminRouting
{
    using Areas.Admin.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Areas.Admin.AdminConstants;

    public class CatsControllerTest
    {
        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithPath("/Admin/Cats/All"))
                .To<CatsController>(c => c.All(null));

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithPath("/Admin/Cats/Edit"))
                .To<CatsController>(c => c.Edit(null));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithPath("/Admin/Cats/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<CatsController>(c => c.Edit(null));

        [Fact]
        public void RestoreRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithPath("/Admin/Cats/All"))
                .To<CatsController>(c => c.All(null));
    }
}