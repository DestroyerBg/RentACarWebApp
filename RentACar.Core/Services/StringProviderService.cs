using Microsoft.Extensions.Configuration;
using RentACar.Core.Interfaces;

namespace RentACar.Core.Services
{
    public class StringProviderService : IStringProvider
    {
        private readonly IConfiguration configuration;

        public StringProviderService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string GetConnectionString()
        {
            string? connectionString = configuration.GetConnectionString("Development");

            return connectionString;
        }

        public string GetGeolocationApiKey()
        {
            string? apiKey = configuration.GetValue<string>("ApiKeys:GeolocationApiKey");

            return apiKey;
        }
    }
}
