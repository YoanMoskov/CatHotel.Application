﻿namespace CatHotel.Test.Routing
{
    using CatHotel.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ReservationsControllerTest
    {
        [Fact]
        public void CreateRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("Reservations/Create")
                .To<ReservationsController>(c => c.Create());

        [Fact]
        public void PostCreateRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Reservations/Create")
                    .WithMethod(HttpMethod.Post))
                .To<ReservationsController>(c => c.Create());

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Reservations/All")
                .To<ReservationsController>(c => c.All());
    }
}