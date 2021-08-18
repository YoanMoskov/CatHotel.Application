namespace CatHotel.Test.AdminControllers
{
    using System.Linq;
    using Areas.Admin.Controllers;
    using Areas.Admin.Models.Enums.Groomings;
    using Areas.Admin.Models.Groomings;
    using CatHotel.Data.Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Groomings;
    using static Areas.Admin.AdminConstants;

    public class GroomingsControllerTest
    {
        [Fact]
        public void GetAllShouldReturnViewWithModel()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName)))
                .Calling(c => c.All(new AdminAllGroomingsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllGroomingsQueryModel>());

        [Fact]
        public void GetAllWithExpiredGroomingShouldReturnViewWithoutExpired()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestMoveToExpiredGrooming))))
                .Calling(c => c.All(new AdminAllGroomingsQueryModel()))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Grooming>(groomings => groomings
                        .Any(g =>
                            g.Id == TestMoveToExpiredGrooming.Id &&
                            g.IsExpired)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllGroomingsQueryModel>());

        [Fact]
        public void GetAllWithStyleFilterAndSortShouldReturnViewWithModelsMatchingTheStyleFilterAndSort()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestApprovedGrooming))))
                .Calling(c => c.All(new AdminAllGroomingsQueryModel
                {
                    StyleId = TestApprovedGrooming.StyleId.ToString(),
                    Filtering = GroomsFiltering.Approved,
                    Sorting = GroomsSorting.Oldest
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllGroomingsQueryModel>(q =>
                        q.StyleId == TestApprovedGrooming.StyleId.ToString() &&
                        q.Filtering == GroomsFiltering.Approved &&
                        q.Sorting == GroomsSorting.Oldest));
        [Fact]

        public void GetAllWithFilterAndSortShouldReturnViewWithModelsMatchingTheFilterAndSort()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestApprovedGrooming))))
                .Calling(c => c.All(new AdminAllGroomingsQueryModel
                {
                    Filtering = GroomsFiltering.Expired,
                    Sorting = GroomsSorting.Oldest
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AdminAllGroomingsQueryModel>(q =>
                        q.Filtering == GroomsFiltering.Expired &&
                        q.Sorting == GroomsSorting.Oldest));

        [Fact]
        public void GetApproveShouldRedirectToAction()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName))
                    .WithData(data => data
                        .WithEntities(entities => entities
                            .AddRange(TestGrooming))))
                .Calling(c => c.Approve(TestGrooming.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Grooming>(groomings => groomings
                        .Any(g =>
                            g.Id == TestGrooming.Id &&
                            g.IsApproved)));

        [Fact]
        public void GetApproveWithInvalidIdShouldReturnBadRequest()
            => MyController<GroomingsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminRoleName)))
                .Calling(c => c.Approve(TestGrooming.Id))
                .ShouldReturn()
                .BadRequest();
    }
}