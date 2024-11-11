using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Seeder;

namespace RentACar.Web.Extensions
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

            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

            return app;
        }
    }
}
