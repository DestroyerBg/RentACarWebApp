using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Helpers;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasData(SeedData());
        }

        private List<Location> SeedData()
        {
            string jsonContent = JsonReader.ReadJson("Locations.json");

            List<Location>? locations = JsonSerializer.Deserialize<List<Location>>(jsonContent);

            locations.ForEach(l => l.Id = Guid.NewGuid());

            return locations;
        }
    }
}
