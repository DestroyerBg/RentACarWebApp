using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Data.Factories
{
    public class RentACarDbContextDesignFactory : IDesignTimeDbContextFactory<RentACarDbContext>
    {
        public RentACarDbContext CreateDbContext(string[] args)
        {
            string webProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "../RentACar.Web");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(webProjectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            string? connectionString = configuration.GetConnectionString("Development");

            DbContextOptionsBuilder<RentACarDbContext> optionsBuilder = new DbContextOptionsBuilder<RentACarDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RentACarDbContext(optionsBuilder.Options);
        }
    }
}
