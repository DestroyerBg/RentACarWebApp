using Microsoft.AspNetCore.Mvc;
namespace RentACar.Web.API.Controllers
{
    public class SmsController : BaseApiController
    {
        [HttpPost("send-sms-message/{phoneNumber?}")]
        public async Task<IActionResult> SendSmsMessage([FromBody] string phoneNumber)
        {
            if (!base.ValidatePhoneNumber(phoneNumber))
            {
                return BadRequest(new {message = "Невалиден формат на телефонен номер"});
            }

            return Ok(new {message = $"Телефонният номер {phoneNumber} e валидиран успешно"});
        }
    }
}
