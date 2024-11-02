using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using static RentACar.Common.ErrorMessages.DatabaseErrorMessages;
namespace RentACar.Web.Extensions
{ 
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

            builder.Services.AddDbContext<RentACarDbContext>(
                options => options.UseSqlServer(connectionString));

            return builder;
        }

        public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<RentACarDbContext>()
                .AddDefaultTokenProviders();

            return builder;
        }
    }
}
