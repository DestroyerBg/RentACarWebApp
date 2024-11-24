using Microsoft.Extensions.Configuration;

namespace RentACar.Core.Interfaces
{
    public interface ISmsService
    {
        Task SendSms(string toPhoneNumber, string message);
    }
}
