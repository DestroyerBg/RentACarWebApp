using Microsoft.AspNetCore.Mvc;
namespace RentACar.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected bool ValidatePhoneNumber(string phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            return true;
        }
    }
}
