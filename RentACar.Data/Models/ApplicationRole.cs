using Microsoft.AspNetCore.Identity;

namespace RentACar.Data.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public override string? ConcurrencyStamp { get; set; }
    }
}
