using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;
using static RentACar.Common.Constants.DatabaseModelsConstants.Common;

namespace RentACar.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly IAdminService adminService;

        public HomeController(IAdminService _adminService)
        {
            adminService = _adminService;
        }
        public async Task<IActionResult> Index()
        {
            if (await adminService.IsUserAdmin(User))
            {
                TempData[ShowPrivateOptions] = true;
            }
            return View();
        }
    }
}
