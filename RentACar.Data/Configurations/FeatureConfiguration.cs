using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Helpers;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasData(SeedData());
        }

        private List<Feature> SeedData()
        {
            string jsonContent = JsonReader.ReadJson("Features.json");

            List<Feature>? features = JsonSerializer.Deserialize<List<Feature>>(jsonContent);

            features.ForEach(f => f.Id = Guid.NewGuid());
            return features;
        }
    }
}
