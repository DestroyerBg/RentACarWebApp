using Microsoft.Extensions.Configuration;
using RentACar.Core.Interfaces;

namespace RentACar.Web.Infrastructure.Providers
{
    public class StringProvider : IStringProvider
    {
        private readonly IConfiguration configuration;

        public StringProvider(IConfiguration _configuration)
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
