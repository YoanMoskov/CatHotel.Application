namespace CatHotel.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(AdminConstants.AdminAreaName)]
    [Authorize(Roles = AdminConstants.AdminRoleName)]
    public abstract class AdminController : Controller
    {
    }
}