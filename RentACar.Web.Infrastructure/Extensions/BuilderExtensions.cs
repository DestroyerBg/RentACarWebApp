using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentACar.Core.Interfaces;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Web.Infrastructure.ErrorDescribers;
using RentACar.Core.Services;

namespace RentACar.Web.Infrastructure.Extensions
{
    public static class BuilderExtensions
    {
        public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IStringProvider, StringProviderService>();
            builder.Services.AddDbContext<RentACarDbContext>((serviceProvider, options) =>
            {
                IStringProvider connectionStringProvider = serviceProvider.GetRequiredService<IStringProvider>();
                string? connectionString = connectionStringProvider.GetConnectionString();

                options.UseSqlServer(connectionString);
            });
            

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
                .AddRoles<IdentityRole<Guid>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddDefaultUI() // TODO Implement email system to remove default identity pages
                .AddEntityFrameworkStores<RentACarDbContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

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
