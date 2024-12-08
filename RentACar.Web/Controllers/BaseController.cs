using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
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
