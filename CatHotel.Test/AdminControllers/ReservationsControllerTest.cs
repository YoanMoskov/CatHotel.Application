namespace CatHotel.Test.AdminControllers
{
    using System.Linq;
    using Areas.Admin.Controllers;
    using Areas.Admin.Models.Enums.Reservations;
    using Areas.Admin.Models.Reservations;
    using CatHotel.Data.Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Reservations;
    using static Areas.Admin.AdminConstants;

    public class ReservationsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithModel()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName)))
                .Calling(c => c.All(new AdminAllReservationsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllReservationsQueryModel>());

        [Fact]
        public void AllWithRoomTypeShouldReturnViewWithModelMatchingRoomType()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestPendingApprovalReservation))))
                .Calling(c => c.All(new AdminAllReservationsQueryModel
                {
                    RoomType = TestPendingApprovalReservation.RoomType.Id.ToString(),
                    Filtering = ResFiltering.PendingApproval,
                    Sorting = ResSorting.Newest
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllReservationsQueryModel>(q =>
                        q.RoomType == TestPendingApprovalReservation.Id &&
                        q.Filtering == ResFiltering.PendingApproval &&
                        q.Sorting == ResSorting.Newest &&
                        q.TotalReservations == 1));

        [Fact]
        public void AllWithFilteringActiveAndSortingOldestShouldWithReturnViewWithModelWithMatchingFilteringSorting()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestActiveReservation))))
                .Calling(c => c.All(new AdminAllReservationsQueryModel
                {
                    RoomType = TestActiveReservation.RoomType.Id.ToString(),
                    Filtering = ResFiltering.Active,
                    Sorting = ResSorting.Oldest
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllReservationsQueryModel>(q =>
                        q.RoomType == TestPendingApprovalReservation.Id &&
                        q.Filtering == ResFiltering.Active &&
                        q.Sorting == ResSorting.Oldest &&
                        q.TotalReservations == 1));

        [Fact]
        public void AllWithFilteringExpiredShouldWithReturnViewWithModelWithMatchingFiltering()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestExpiredReservation))))
                .Calling(c => c.All(new AdminAllReservationsQueryModel
                {
                    RoomType = TestActiveReservation.RoomType.Id.ToString(),
                    Filtering = ResFiltering.Expired
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllReservationsQueryModel>(q =>
                        q.RoomType == TestPendingApprovalReservation.Id &&
                        q.Filtering == ResFiltering.Expired &&
                        q.TotalReservations == 1));

        [Fact]
        public void AllWithFilteringPendingShouldWithReturnViewWithModelWithMatchingFiltering()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestPendingReservation))))
                .Calling(c => c.All(new AdminAllReservationsQueryModel
                {
                    RoomType = TestActiveReservation.RoomType.Id.ToString(),
                    Filtering = ResFiltering.Pending
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllReservationsQueryModel>(q =>
                        q.RoomType == TestPendingApprovalReservation.Id &&
                        q.Filtering == ResFiltering.Pending &&
                        q.TotalReservations == 1));

        [Fact]
        public void AllWithFilteringActiveShouldWithReturnViewWithModelWithMatchingFiltering()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestApprovedReservation))))
                .Calling(c => c.All(new AdminAllReservationsQueryModel
                {
                    RoomType = TestActiveReservation.RoomType.Id.ToString(),
                    Filtering = ResFiltering.Approved
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllReservationsQueryModel>(q =>
                        q.RoomType == TestPendingApprovalReservation.Id &&
                        q.Filtering == ResFiltering.Approved &&
                        q.Reservations.Any(r => r.IsApproved) &&
                        q.TotalReservations == 1));

        [Fact]
        public void ApproveWithInvalidIdShouldReturnBadRequest()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestPendingApprovalReservation))))
                .Calling(c => c.Approve("Invalid"))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void ApproveWithReservationIdShouldChangeIsApprovedToTrueAndRedirectToAction()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestPendingApprovalReservation))))
                .Calling(c => c.Approve(TestReservation.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Reservation>(reservations => reservations
                        .Any(r =>
                            r.IsApproved &&
                            r.RoomType == TestPendingApprovalReservation.RoomType &&
                            r.Id == TestPendingApprovalReservation.Id)))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
    }
}