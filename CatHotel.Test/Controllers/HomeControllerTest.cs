namespace CatHotel.Test.Controllers
{
    using CatHotel.Controllers;
    using Models;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithNoModel()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(h => h.Index())
                .Which(controller => controller
                    .WithUser("TestUser"))
                .ShouldReturn()
                .View(view => view
                    .WithNoModel());

        [Fact]
        public void ErrorShouldReturnViewWithModel()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(a => a.Error())
                .Which()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ErrorViewModel>());
    }
}