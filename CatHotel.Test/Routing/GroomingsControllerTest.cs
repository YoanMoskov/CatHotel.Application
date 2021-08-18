namespace CatHotel.Test.Routing
{
    using CatHotel.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using static Data.Groomings;
    using static Data.Cats;

    public class GroomingsControllerTest
    {
        [Fact]
        public void StylesRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Groomings/Styles")
                .To<GroomingsController>(c => c.Styles());

        [Fact]
        public void CatsRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Groomings/Cats?styleId=1")
                .To<GroomingsController>(c => c.Cats(TestStyle.Id));

        [Fact]
        public void CompleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Groomings/Complete?styleId=1&catId=1")
                .To<GroomingsController>(c => c.Complete(TestStyle.Id, TestCat.Id));

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Groomings/All")
                .To<GroomingsController>(c => c.All());
    }
}