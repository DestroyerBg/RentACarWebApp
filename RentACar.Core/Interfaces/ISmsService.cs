using Microsoft.Extensions.Configuration;

namespace RentACar.Core.Interfaces
{
    public interface ISmsService
    {
        Task SendSms(IConfiguration configuration, string toPhoneNumber, string message);
    }
}
