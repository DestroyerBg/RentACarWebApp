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
        public string? GetConnectionString()
        {
            string? connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = configuration.GetConnectionString("Development");
            }

            return connectionString;
        }
    }
}
