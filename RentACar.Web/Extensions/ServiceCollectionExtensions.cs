using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using RentACar.Data.Models;

namespace RentACar.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService<ApplicationUser, Guid>, ApplicationUserService>();

            return services;
        }
    }
}
