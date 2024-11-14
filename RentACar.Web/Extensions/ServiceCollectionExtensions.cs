﻿using RentACar.Core.Infrastructure.AutoMapperProfiles;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;

namespace RentACar.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService<ApplicationUser, Guid>, ApplicationUserService>();
            services.AddScoped<ICarService, CarService>();
            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IdentityProfiles));
            services.AddAutoMapper(typeof(CarProfiles));
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Car, Guid>, BaseRepository<Car, Guid>>();

            return services;
        }

    }
}
