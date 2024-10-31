using Microsoft.EntityFrameworkCore;
using RentACar.Data;

namespace RentACar.Web.Extensions
{
    using static RentACar.Common.ErrorMessages.DatabaseErrorMessages;

    public static class BuilderExtensions
    {
        public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("secrets.json", true)
                .AddUserSecrets<Program>();

            string? connectionString = builder.Configuration.GetConnectionString("Development")
                ?? throw new NullReferenceException(connectionStringNotAvailable);

            builder.Services.AddDbContext<RentACarDbContext>(options => options.UseSqlServer(connectionString));

            return builder;
        } 
    }
}
