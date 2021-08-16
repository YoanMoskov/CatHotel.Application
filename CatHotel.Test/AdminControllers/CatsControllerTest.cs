namespace CatHotel.Test.AdminControllers
{
    using System.Linq;
    using CatHotel.Areas.Admin.Models.Enums.Cats;
    using CatHotel.Data.Models;
    using Areas.Admin.Controllers;
    using Areas.Admin.Models.Cats;
    using MyTested.AspNetCore.Mvc;
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
        public void AllWithFilteringAvailableAndSortingOldestShouldWithReturnViewWithModelWithMatchingFilteringSorting()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestAvailableCat, TestBreed))))
                .Calling(c => c.All(new AdminAllCatsQueryModel()
                {
                    Breed = TestCat.Breed.Id.ToString(),
                    Filtering = CatFiltering.Available,
                    Sorting = CatSorting.Oldest
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllCatsQueryModel>(c =>
                        c.Breed == TestCat.Breed.Id.ToString() &&
                        c.Filtering == CatFiltering.Available &&
                        c.Sorting == CatSorting.Oldest &&
                        c.TotalCats == 1));

        [Fact]
        public void AllWithFilteringDeletedShouldWithReturnViewWithModelWithMatchingFiltering()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestDeletedCatCat, TestBreed))))
                .Calling(c => c.All(new AdminAllCatsQueryModel()
                {
                    Breed = TestCat.Breed.Id.ToString(),
                    Filtering = CatFiltering.Deleted
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllCatsQueryModel>(c =>
                        c.Breed == TestCat.Breed.Id.ToString() &&
                        c.Filtering == CatFiltering.Deleted &&
                        c.TotalCats == 1));

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
        public void EditWithInvalidIdShouldReturnBadRequest()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName)))
                .Calling(c => c.Edit("InvalidId"))
                .ShouldReturn()
                .BadRequest();

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
        public void PostEditWithInvalidIdShouldAndReturnBadRequest()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName)))
                .Calling(c => c.Edit(new AdminEditCatFormModel(), "Invalid"))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

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
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Restore(TestCat.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Cat>(cats => cats
                        .Any(c =>
                            c.Id == TestCat.Id &&
                            c.IsDeleted == false)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void RestoreWithInvalidIdShouldReturnBadRequest()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Restore("Invalid"))
                .ShouldReturn()
                .BadRequest();
    }
}