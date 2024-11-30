using Microsoft.AspNetCore.Mvc;
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
