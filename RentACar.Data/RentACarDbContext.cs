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
        }
    }
}
