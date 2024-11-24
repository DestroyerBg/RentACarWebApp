using Microsoft.Extensions.Configuration;
using RentACar.Core.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RentACar.Core.Services
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration configuration;

        public SmsService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public async Task SendSms(string toPhoneNumber, string message)
        {
            string accoundSID = configuration["Twilio:AccountSID"];
            string authToken = configuration["Twilio:AuthToken"];
            string fromPhoneNumber = configuration["Twilio:FromPhoneNumber"];

            TwilioClient.Init(accoundSID, authToken);

            MessageResource? messageResult = await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(fromPhoneNumber),
                to: new PhoneNumber(toPhoneNumber)
            );
        }
    }
}
