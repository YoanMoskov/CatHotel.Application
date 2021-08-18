namespace CatHotel.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CatHotel.Controllers;
    using CatHotel.Data.Models;
    using Models.Grooming.FormModel;
    using MyTested.AspNetCore.Mvc;
    using Services.Models.Groomings.CommonArea;
    using Xunit;
    using static Data.Groomings;
    using static Data.Cats;
    using static WebConstants;

    public class GroomingsControllerTest
    {
        [Fact]
        public void GetStylesShouldReturnViewWithModel()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestStyle, TestCat))))
                .Calling(c => c.Styles())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntryWithKey(stylesCacheKey))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<GroomingStyleServiceModel>>(styles => styles
                        .Any(s =>
                            s.Name == TestStyle.Name &&
                            s.PhotoUrl == TestStyle.PhotoUrl)));

        [Fact]
        public void GetStylesWhenUserWithoutCatsShouldRedirectToAction()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.Styles())
                .ShouldHave()
                .TempData(tdata => tdata
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Add", "Cats");

        [Fact]
        public void GetCatsShouldReturnViewWithModel()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Cats(2))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<GroomingCatServiceModel>>(cats => cats.Count() == 1));

        [Fact]
        public void GetCompleteShouldReturnViewWithModel()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat, TestStyle))))
                .Calling(c => c.Complete(TestStyle.Id, TestCat.Id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddGroomingModel>(g =>
                        g.Cat.Id == TestCat.Id &&
                        g.Style.Id == TestStyle.Id));

        [Fact]
        public void PostCompleteShouldReturnRedirectToAction()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat, TestStyle))))
                .Calling(c => c.Complete(new AddGroomingModel
                {
                    Appointment = DateTime.UtcNow.AddDays(2)
                }, TestStyle.Id, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Grooming>(groomings => groomings
                        .Any(g =>
                            g.StyleId == TestStyle.Id &&
                            g.CatId == TestCat.Id &&
                            g.IsApproved == false)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void PostCompleteWithInvalidStyleShouldReturnBadRequest()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Complete(null, TestStyle.Id, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void PostCompleteWithInvalidCatShouldReturnBadRequest()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestStyle))))
                .Calling(c => c.Complete(null, TestStyle.Id, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void PostCompleteWithInvalidModelDataShouldHaveInvalidModelStateAndReturnView()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat, TestStyle))))
                .Calling(c => c.Complete(new AddGroomingModel
                {
                    Appointment = DateTime.UtcNow.AddDays(-2)
                }, TestStyle.Id, TestCat.Id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddGroomingModel>());

        [Fact]
        public void GetAllShouldReturnViewWithModel()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestGrooming))))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<GroomingServiceModel>>(g => g.Count() == 1));
    }
}