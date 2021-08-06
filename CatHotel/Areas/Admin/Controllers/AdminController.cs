namespace CatHotel.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.RoleName)]
    public abstract class AdminController : Controller
    {
    }
}