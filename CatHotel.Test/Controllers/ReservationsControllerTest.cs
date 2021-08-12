namespace CatHotel.Test.Controllers
{
    using System.Collections.Generic;
    using Areas.Admin;
    using CatHotel.Controllers;
    using Models.Reservation.FormModels;
    using MyTested.AspNetCore.Mvc;
    using Services.Models.Reservations.CommonArea;
    using Xunit;

    public class ReservationsControllerTest
    {
        [Fact]
        public void GetCreateShouldReturnViewWithModel()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Reservations/Create")
                    .WithUser())
                .To<ReservationsController>(c => c.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ResFormModel>());

        [Fact]
        public void GetActiveShouldReturnViewWithModel()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Reservations/Active")
                    .WithUser())
                .To<ReservationsController>(c => c.Active())
                .Which()
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>());

        [Fact]
        public void GetActiveShouldRedirectIfUserInRoleAdmin()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminConstants.RoleName)))
                .Calling(c => c.Active())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrl("/Admin/Reservations/All"));

        [Fact]
        public void GetApprovedShouldReturnViewWithModel()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Reservations/Approved")
                    .WithUser())
                .To<ReservationsController>(c => c.Approved())
                .Which()
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>());

        [Fact]
        public void GetApprovedShouldRedirectIfUserInRoleAdmin()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminConstants.RoleName)))
                .Calling(c => c.Approved())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrl("/Admin/Reservations/All"));

        [Fact]
        public void GetPendingApprovalShouldReturnViewWithModel()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Reservations/PendingApproval")
                    .WithUser())
                .To<ReservationsController>(c => c.PendingApproval())
                .Which()
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ResServiceModel>>());

        [Fact]
        public void GetPendingApprovalShouldRedirectIfUserInRoleAdmin()
            => MyController<ReservationsController>
                .Instance(controller => controller
                    .WithUser(u => u.InRole(AdminConstants.RoleName)))
                .Calling(c => c.PendingApproval())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrl("/Admin/Reservations/All"));
    }
}