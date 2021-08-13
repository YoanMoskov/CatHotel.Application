namespace CatHotel.Test.Routing
{
    using CatHotel.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CatsControllerTest
    {
        [Fact]
        public void AddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cats/Add")
                .To<CatsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithPath("/Cats/Add"))
                .To<CatsController>(c => c.Add());

        [Fact]
        public void AllRouteShouldBeMapped()
             => MyRouting
                .Configuration()
                .ShouldMap("/Cats/All")
                .To<CatsController>(c => c.All());

        [Fact]
        public void PostAllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithPath("/Cats/All"))
                .To<CatsController>(c => c.All());

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cats/Edit")
                .To<CatsController>(c => c.Edit(null));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithPath("/Cats/Edit"))
                .To<CatsController>(c => c.Edit(null));

        [Fact]
        public void DeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cats/Delete")
                .To<CatsController>(c => c.Delete(null));
    }
}