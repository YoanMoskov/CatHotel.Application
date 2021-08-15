namespace CatHotel.Test.Controllers
{
    using CatHotel.Controllers;
    using CatHotel.Data.Models;
    using Models.Reservation.FormModels;
    using MyTested.AspNetCore.Mvc;
    using Services.Models.Reservations.CommonArea;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CatHotel.Data.Models.Enums;
    using Xunit;

    using static Data.Reservations;
    using static Data.Cats;
    using static WebConstants;

    public class ReservationsControllerTest
    {
        [Fact]
        public void CreateShouldBeForUserRoleAndReturnViewWithModel()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)))
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ResFormModel>());

        [Theory]
        [InlineData(1, 2)]
        public void PostCreateCatShouldBeForUserRoleAndReturnRedirectWithValidModel(
            double daysAddArrival,
            double daysAddDeparture)
        {
            var arrival = DateTime.UtcNow.AddDays(daysAddArrival);
            var departure = DateTime.UtcNow.AddDays(daysAddDeparture);

            var roomTypeId = 1;
            var catIds = new string[] { "1" };

            MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(UserRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities.AddRange(
                            TestCat, TestRoom))))
                .Calling(c => c.Create(new ResFormModel()
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
                .RedirectToAction("PendingApproval");
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
                .Calling(c => c.Create(new ResFormModel()
                {
                    CatIds = new[] { "nonexistent" }
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
                .Calling(c => c.Create(new ResFormModel()
                {
                    CatIds = new[] { TestCat.Id },
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
                .Calling(c => c.Create(new ResFormModel()
                {
                    RoomTypeId = TestRoom.Id,
                    CatIds = new[] { TestCat.Id },
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
                            .AddRange(TestReservation, TestCatReservations[0], TestCatReservations[1], TestRoom, TestCats[0], TestCats[1]))))
                .Calling(c => c.Create(new ResFormModel()
                {
                    RoomTypeId = TestRoom.Id,
                    CatIds = new[] { TestCats[0].Id, TestCats[1].Id },
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
        public void GetActiveShouldBeForUserRoleAndReturnViewWithModel()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)).
                    WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestActiveReservation))))
                .Calling(c => c.Active())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>(model => model.Count == 1));

        [Fact]
        public void GetActiveWithPendingReservationShouldBecomeActive()
           => MyController<ReservationsController>
               .Instance(controller => controller
                   .WithUser(c => c.InRole(UserRoleName)).
                   WithData(data => data
                       .WithEntities(entities => entities
                           .AddRange(TestActiveFromPendingReservation))))
               .Calling(c => c.Active())
               .ShouldHave()
               .Data(data => data
                   .WithSet<Reservation>(reservations => reservations
                       .Any(r =>
                           r.ReservationState == ReservationState.Active)))
               .AndAlso()
               .ShouldReturn()
               .View(view => view
                   .WithModelOfType<List<ResServiceModel>>(model => model.Count == 1));

        [Fact]
        public void GetExpiredWithActiveReservationShouldBecomeExpired()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(c => c.InRole(UserRoleName)).
                    WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestExpiredFromActiveReservation))))
                .Calling(c => c.Active())
                .ShouldHave()
                .Data(data => data
                    .WithSet<Reservation>(reservations => reservations
                        .Any(r =>
                            r.ReservationState == ReservationState.Expired)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>(model => model.Count == 0));

        [Fact]
        public void GetApprovedShouldBeForUserRoleAndReturnViewWithModel()
             => MyController<ReservationsController>
                 .Instance(controller => controller
                     .WithUser(c => c.InRole(UserRoleName)))
                 .Calling(c => c.Approved())
                 .ShouldReturn()
                 .View(view => view
                     .WithModelOfType<List<ResServiceModel>>());

        [Fact]
        public void GetPendingApprovalShouldBeForUserRoleAndReturnViewWithModel()
             => MyController<ReservationsController>
                 .Instance(controller => controller
                     .WithUser(c => c.InRole(UserRoleName)))
                 .Calling(c => c.PendingApproval())
                 .ShouldReturn()
                 .View(view => view
                     .WithModelOfType<List<ResServiceModel>>());
    }
}