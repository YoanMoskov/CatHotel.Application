namespace CatHotel.Test.Controllers
{
    using CatHotel.Controllers;
    using Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexShouldReturnViewWithNoModel()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithNoModel());

        [Fact]
        public void GetErrorShouldReturnViewWithModel()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ErrorViewModel>());
    }
}