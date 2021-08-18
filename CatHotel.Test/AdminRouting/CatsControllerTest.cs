namespace CatHotel.Test.AdminRouting
{
    using Areas.Admin.Controllers;
    using Areas.Admin.Models.Cats;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Cats;
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
                .To<CatsController>(c => c.All(new AdminAllCatsQueryModel()));

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithLocation("/Admin/Cats/Edit?catId=1"))
                .To<CatsController>(c => c.Edit(TestCat.Id));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithLocation("/Admin/Cats/Edit?catId=1")
                    .WithMethod(HttpMethod.Post))
                .To<CatsController>(c => c.Edit(TestCat.Id));

        [Fact]
        public void RestoreRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithLocation("/Admin/Cats/Restore?catId=1"))
                .To<CatsController>(c => c.Restore(TestCat.Id));
    }
}