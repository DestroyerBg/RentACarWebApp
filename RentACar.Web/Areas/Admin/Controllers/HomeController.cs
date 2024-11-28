using Microsoft.AspNetCore.Mvc;
using RentACar.Web.Controllers;

namespace RentACar.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
