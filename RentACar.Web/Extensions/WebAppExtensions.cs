using System.ServiceProcess;
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
    }
}
