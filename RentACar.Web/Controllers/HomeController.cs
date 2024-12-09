using Microsoft.AspNetCore.Mvc;
namespace RentACar.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401 || statusCode == 403)
            {
                return View("Error401");
            }

            if (statusCode == 500 || statusCode == null) 
            {
                return View("Error500");
            }

            return View();

        }
    }
}
