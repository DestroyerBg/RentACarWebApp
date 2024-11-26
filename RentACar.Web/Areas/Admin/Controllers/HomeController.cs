using Microsoft.AspNetCore.Mvc;
using RentACar.Web.Controllers;

namespace RentACar.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
