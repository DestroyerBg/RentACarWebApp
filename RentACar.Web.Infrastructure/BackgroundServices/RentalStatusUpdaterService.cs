using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentACar.Data;
using RentACar.Data.Models;

namespace RentACar.Web.Infrastructure.BackgroundServices
{
    public class RentalStatusUpdaterService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public RentalStatusUpdaterService(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateCarRentalStatus();
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task UpdateCarRentalStatus()
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                Console.WriteLine($"Updating cars status...");
                RentACarDbContext? context = scope.ServiceProvider.GetService<RentACarDbContext>();

                DateTime today = DateTime.Now.Date;

                IList<Car> cars =  context
                    .Cars
                    .Include(c => c.Reservations)
                    .Where(c => c.IsHired && c.Reservations.Any(r => r.EndDate < today))
                    .ToList();

                foreach (Car car in cars)
                {
                    car.IsHired = false;
                    car.LastReservationDate = DateTime.Now.Date.AddDays(-1);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
