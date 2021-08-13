namespace CatHotel.Test.Controllers
{
    using CatHotel.Controllers;
    using CatHotel.Data.Models;
    using Models.Cat.FormModel;
    using MyTested.AspNetCore.Mvc;
    using Services.Models.Cats.CommonArea;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using static Data.Cats;
    using static WebConstants;

    public class CatsControllerTest
    {
        [Fact]
        public void AddShouldBeForUserRoleAndReturnViewWithModel()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)))
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddCatFormModel>());

        [Fact]
        public void PostAddShouldBeAddingCatAndRedirectingToAction()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestBreed))))
                .Calling(c => c.Add(new AddCatFormModel()
                {
                    Name = TestCat.Name,
                    Age = TestCat.Age,
                    PhotoUrl = TestCat.PhotoUrl,
                    BreedId = TestCat.BreedId
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Cat>(cat =>
                        cat.Any(c =>
                            c.Name == TestCat.Name &&
                            c.Age == TestCat.Age &&
                            c.PhotoUrl == TestCat.PhotoUrl &&
                            c.BreedId == TestCat.BreedId)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void PostAddShouldBeWithInvalidStateModelIfPassedBreedDoesNotExistAndReturnView()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.Add(new AddCatFormModel()
                {
                    BreedId = 404
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddCatFormModel>());

        [Fact]
        public void AllShouldBeForUserRoleAndReturnViewWithModel()
            => MyController<CatsController>
                .Instance(controller => controller
                .WithUser(c => c.InRole(UserRoleName))
                .WithData(data => data
                    .WithEntities(entities => entities
                        .AddRange(TestCat))))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<List<CatServiceModel>>());

        [Fact]
        public void AllShouldBeRedirectingIfUserHasNoCats()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.All())
                .ShouldReturn()
                .RedirectToAction("Add");

        [Fact]
        public void EditShouldBeForUserRoleAndReturnViewWithModel()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Edit(TestCat.Id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CatServiceModel>());

        [Fact]
        public void PostEditShouldBeChangingTheOriginalDataAndRedirectingToAction()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Edit(new EditCatFormModel()
                {
                    Age = TestCat.Age + 1,
                    PhotoUrl = TestCat.PhotoUrl
                }, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .Data(data => data
                    .WithSet<Cat>(cat =>
                        cat.Any(c =>
                            c.Age == TestCat.Age + 1)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void PostEditShouldReturnBadRequestIfModelIsNull()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.Edit(null, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void PostEditShouldHaveInvalidModelStateIfModelParamsInvalid()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.Edit(new EditCatFormModel()
                {
                    Age = 0,
                    PhotoUrl = "InvalidUrl"
                }, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .InvalidModelState();

        [Fact]
        public void DeleteShouldBeForUserRoleAndReturnNoView()
            => MyController<CatsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)))
                .Calling(c => c.Delete(null))
                .ShouldReturn()
                .RedirectToAction("All");
    }
}