﻿using Microsoft.Extensions.DependencyInjection;
using RentACar.Core.Infrastructure.AutoMapperProfiles;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;
using RentACar.Web.Infrastructure.BackgroundServices;

namespace RentACar.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, ApplicationUserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICustomerFeedbackService, CustomerFeedbackService>();
            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(CarProfiles).Assembly);
            });
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

            return services;
        }

        public static IServiceCollection RegisterRentalStatusUpdaterService(this IServiceCollection services)
        {
            services.AddHostedService<RentalStatusUpdaterService>();

            return services;
        }

    }
}
