using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    public class CarFeatureConfiguration : IEntityTypeConfiguration<CarFeature>
    {
        public void Configure(EntityTypeBuilder<CarFeature> builder)
        {
            builder.HasKey(pk => new { pk.CarId, pk.FeatureId });

            builder.HasOne(c => c.Car)
                .WithMany(cf => cf.CarFeatures);

            builder.HasOne(f => f.Feature)
                .WithMany(cf => cf.CarFeatures);
        }
    }
}
