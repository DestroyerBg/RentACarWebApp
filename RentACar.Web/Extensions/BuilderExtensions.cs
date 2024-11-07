using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Web.ErrorDescribers;
using static RentACar.Common.Messages.ErrorMessages.DatabaseErrorMessages;
namespace RentACar.Web.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("Development")
                ?? throw new NullReferenceException(connectionStringNotAvailable);

            builder.Services.AddDbContext<RentACarDbContext>(
                options => options.UseSqlServer(connectionString));

            return builder;
        }

        public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
        {
            IConfigurationSection settings = builder.Configuration.GetSection("Identity");
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
                {
                    ConfigureIdentity(options, settings);
                })
                .AddDefaultUI()
                .AddEntityFrameworkStores<RentACarDbContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            return builder;
        }

        private static void ConfigureIdentity(IdentityOptions options, IConfigurationSection configurationSettings)
        {
            //Password
            options.Password.RequireDigit = configurationSettings.GetValue<bool>("Password:RequireDigit");
            options.Password.RequiredLength = configurationSettings.GetValue<int>("Password:RequiredLength");
            options.Password.RequireNonAlphanumeric = configurationSettings.GetValue<bool>("Password:RequireNonAlphanumeric");
            options.Password.RequireUppercase = configurationSettings.GetValue<bool>("Password:RequireUppercase");
            options.Password.RequireLowercase = configurationSettings.GetValue<bool>("Password:RequireLowercase");

            //Lockout
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(
                configurationSettings.GetValue<int>("Lockout:DefaultLockoutTimeSpanInMinutes"));
            options.Lockout.MaxFailedAccessAttempts =
                configurationSettings.GetValue<int>("Lockout:MaxFailedAccessAttempts");
            options.Lockout.AllowedForNewUsers =
                configurationSettings.GetValue<bool>("Lockout:AllowedForNewUsers");
        }
    }
}
