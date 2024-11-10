using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;

namespace RentACar.Data
{
    public class RentACarDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>
    {
        public RentACarDbContext(DbContextOptions<RentACarDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<CarFeature> CarsFeatures { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<InsuranceBenefit> InsuranceBenefits { get; set; }

        public DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
