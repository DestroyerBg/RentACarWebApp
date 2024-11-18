using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentACar.Data;
using RentACar.Data.Helpers;
using RentACar.Data.Models;
using RentACar.Data.Seeder;

namespace RentACar.Web.Infrastructure.Extensions
{
    public static class WebAppExtensions
    {
        public static async Task<WebApplication> SeedAdminAndRoles(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                await IdentitySeeder.SeedRoles(serviceProvider);
            }

            return app;
        }

        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            RentACarDbContext dbContext = serviceScope
                .ServiceProvider
                .GetRequiredService<RentACarDbContext>();

            dbContext.Database.Migrate();

            return app;
        }

        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            RentACarDbContext dbContext = serviceScope
                .ServiceProvider
                .GetRequiredService<RentACarDbContext>();

            SeedEntity<Location>(dbContext, "Locations.json");
            SeedEntity<InsuranceBenefit>(dbContext, "InsuranceBenefits.json");
            SeedEntity<Feature>(dbContext, "Features.json");
            SeedEntity<Category>(dbContext, "Categories.json");
            SeedEntity<Car>(dbContext, "Cars.json");
            SeedEntity<CarFeature>(dbContext, "CarFeatures.json");

            return app;
        }

        private static void SeedEntity<T>(RentACarDbContext dbContext, string jsonFile)
        where T : class
        {
            string jsonContent = JsonReader.ReadJson(jsonFile);

            List<T>? data =  JsonSerializer.Deserialize<List<T>>(jsonContent);

            if (!dbContext.Set<T>().Any() && data != null)
            {
                dbContext.Set<T>().AddRange(data);
            }

            dbContext.SaveChanges();
        }


    }
}
