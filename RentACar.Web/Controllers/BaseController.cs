using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Models;

namespace RentACar.Web.Controllers
{
    public class BaseController : Controller
    {
        public bool IsValidGuid(string guid)
        {
            bool isValid = Guid.TryParse(guid, out Guid result);

            return isValid;
        }
    }
}
