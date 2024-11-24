using Microsoft.AspNetCore.Mvc;
using RentACar.Core.Interfaces;

namespace RentACar.Web.API.Controllers
{
    public class SmsController : BaseApiController
    {
        private readonly IUserService userService;

        public SmsController(IUserService _userService)
        {
            userService = _userService;
        }
        [HttpPost("send-sms-message/{phoneNumber?}")]
        public async Task<IActionResult> SendSmsMessage([FromRoute] string? phoneNumber)
        {
            if (!base.ValidatePhoneNumber(phoneNumber)) 
            {
                return BadRequest(new {message = "Невалиден формат на телефонен номер."});
            }

            string token = userService.GenerateChangePasswordNumberAsync();

            HttpContext.Session.SetString("PasswordResetToken", token);

            return Ok();
        }
    }
}
