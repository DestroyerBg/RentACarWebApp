using Microsoft.Extensions.DependencyInjection;
using RentACar.Core.Infrastructure.AutoMapperProfiles;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;

namespace RentACar.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService<ApplicationUser, Guid>, ApplicationUserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IReservationService, ReservationService>();
            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IdentityProfiles));
            services.AddAutoMapper(typeof(CarProfiles));
            services.AddAutoMapper(typeof(InsuranceBenefitProfiles));
            services.AddAutoMapper(typeof(LocationProfiles));
            services.AddAutoMapper(typeof(ReservationProfiles));
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Car, Guid>, BaseRepository<Car, Guid>>(); services.AddScoped<IRepository<InsuranceBenefit, Guid>, BaseRepository<InsuranceBenefit, Guid>>();
            services.AddScoped<IRepository<Location, Guid>, BaseRepository<Location, Guid>>();
            services.AddScoped<IRepository<Reservation, Guid>, BaseRepository<Reservation, Guid>>();

            return services;
        }

    }
}
