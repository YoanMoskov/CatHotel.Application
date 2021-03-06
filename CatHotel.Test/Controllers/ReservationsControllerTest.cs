namespace CatHotel.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CatHotel.Controllers;
    using CatHotel.Data.Models;
    using CatHotel.Data.Models.Enums;
    using Models.Reservation.FormModels;
    using MyTested.AspNetCore.Mvc;
    using Services.Models.Reservations.CommonArea;
    using Xunit;
    using static Data.Reservations;
    using static Data.Cats;
    using static WebConstants;

    public class ReservationsControllerTest
    {
        [Fact]
        public void GetCreateShouldReturnViewWithModel()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)).WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Create())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntryWithKey(roomTypesCacheKey))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ResFormModel>());

        [Fact]
        public void GetCreateWithUserWithoutCatsShouldRedirectToAction()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)))
                .Calling(c => c.Create())
                .ShouldReturn()
                .RedirectToAction("Add", "Cats");

        [Theory]
        [InlineData(1, 2)]
        public void PostCreateResShouldReturnRedirectWithValidModel(
            double daysAddArrival,
            double daysAddDeparture)
        {
            var arrival = DateTime.UtcNow.AddDays(daysAddArrival);
            var departure = DateTime.UtcNow.AddDays(daysAddDeparture);

            var roomTypeId = 1;
            var catIds = new[] {"1"};

            MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities.AddRange(
                            TestCat, TestRoom))))
                .Calling(c => c.Create(new ResFormModel
                {
                    Arrival = arrival,
                    Departure = departure,
                    RoomTypeId = roomTypeId,
                    CatIds = catIds
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Reservation>(reservations => reservations
                        .Any(r =>
                            r.Arrival == arrival &&
                            r.Departure == departure &&
                            r.RoomTypeId == roomTypeId)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }

        [Fact]
        public void PostCreateShouldReturnBadRequestIfResModelEmpty()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.Create(null))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void PostCreateShouldReturnBadRequestIfNoCats()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName)))
                .Calling(c => c.Create(new ResFormModel
                {
                    CatIds = new[] {"nonexistent"}
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void PostCreateShouldReturnBadRequestIfRoomTypeNotExist()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestCat))))
                .Calling(c => c.Create(new ResFormModel
                {
                    CatIds = new[] {TestCat.Id},
                    RoomTypeId = 404
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void PostCreateShouldBeWithInvalidModelStateIfThereAreCatsInOtherReservationInThisTimeFrame()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestReservation, TestCatReservation, TestRoom, TestCat))))
                .Calling(c => c.Create(new ResFormModel
                {
                    RoomTypeId = TestRoom.Id,
                    CatIds = new[] {TestCat.Id},
                    Arrival = TestReservation.Arrival,
                    Departure = TestReservation.Departure
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ResFormModel>());

        [Fact]
        public void PostCreateWithTwoCatsShouldBeWithInvalidModelStateIfThereAreCatsInOtherReservationInThisTimeFrame()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestReservation, TestCatReservations[0], TestCatReservations[1], TestRoom,
                                TestCats[0], TestCats[1]))))
                .Calling(c => c.Create(new ResFormModel
                {
                    RoomTypeId = TestRoom.Id,
                    CatIds = new[] {TestCats[0].Id, TestCats[1].Id},
                    Arrival = TestReservation.Arrival,
                    Departure = TestReservation.Departure
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ResFormModel>());

        [Fact]
        public void GetAllShouldAndReturnViewWithModel()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)).WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestActiveReservation))))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>(model => model
                        .Any(r =>
                            r.ReservationState == ReservationState.Active)));

        [Fact]
        public void GetAllWithPendingReservationShouldBecomeActive()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)).WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestActiveFromPendingReservation))))
                .Calling(c => c.All())
                .ShouldHave()
                .Data(data => data
                    .WithSet<Reservation>(reservations => reservations
                        .Any(r =>
                            r.ReservationState == ReservationState.Active)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>(model => model
                        .Any(r =>
                            r.ReservationState == ReservationState.Active)));

        [Fact]
        public void GetAllWithActiveReservationShouldBecomeExpired()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)).WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestExpiredFromActiveReservation))))
                .Calling(c => c.All())
                .ShouldHave()
                .Data(data => data
                    .WithSet<Reservation>(reservations => reservations
                        .Any(r =>
                            r.ReservationState == ReservationState.Expired)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>(model => model
                        .Any(r =>
                            r.ReservationState == ReservationState.Expired)));
    }
}