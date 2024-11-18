using Microsoft.Extensions.Configuration;
using RentACar.Web.Infrastructure.Providers.Interfaces;
using System.Configuration;

namespace RentACar.Web.Infrastructure.Providers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration configuration;

        public ConnectionStringProvider(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string GetConnectionString()
        {
            string? connectionString = configuration.GetConnectionString("Development");

            return connectionString;
        }
    }
}
