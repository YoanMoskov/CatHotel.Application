namespace CatHotel.Test.Routing
{
    using CatHotel.Controllers;
    using Models.Cat.FormModel;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Cats;

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
                .ShouldMap("/Cats/Edit?catId=1")
                .To<CatsController>(c => c.Edit(TestCat.Id));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Cats/Edit?catId=1")
                    .WithFormFields(new
                    {
                        Age = TestCat.Age.ToString(), TestCat.PhotoUrl
                    }))
                .To<CatsController>(c => c.Edit(new EditCatFormModel
                {
                    Age = TestCat.Age,
                    PhotoUrl = TestCat.PhotoUrl
                }, TestCat.Id));

        [Fact]
        public void DeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cats/Delete?catId=1")
                .To<CatsController>(c => c.Delete(TestCat.Id));
    }
}