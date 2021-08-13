namespace CatHotel.Test.AdminControllers
{
    using Areas.Admin.Controllers;
    using Areas.Admin.Models.Cats;
    using MyTested.AspNetCore.Mvc;
    using RestSharp;
    using Services.Models.Cats.AdminArea;
    using Xunit;

    using static Data.Cats;
    using static Areas.Admin.AdminConstants;

    public class CatsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithModel()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName)))
                .Calling(c => c.All(new AdminAllCatsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllCatsQueryModel>());

        [Fact]
        public void EditShouldReturnViewWithModel()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Edit(TestCat.Id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminCatEditServiceModel>());

        [Fact]
        public void PostEditShouldPassAndRedirectToAction()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat, TestBreed2))))
                .Calling(c => c.Edit(new AdminEditCatFormModel()
                {
                    Name = "Edited",
                    Age = 10,
                    BreedId = TestBreed.Id
                }, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void PostEditShouldReturnViewWithInvalidModelState()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat, TestBreed2))))
                .Calling(c => c.Edit(new AdminEditCatFormModel()
                {
                    Age = 0
                }, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminCatEditServiceModel>());

        [Fact]
        public void RestoreShouldRedirectToAction()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName)))
                .Calling(c => c.Restore(TestCat.Id))
                .ShouldReturn()
                .RedirectToAction("All");
    }
}