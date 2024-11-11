using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Helpers;
using RentACar.Data.Models;
using System.Text.Json;

namespace RentACar.Data.Configurations
{
    internal class InsuranceBenefitConfiguration : IEntityTypeConfiguration<InsuranceBenefit>
    {
        public void Configure(EntityTypeBuilder<InsuranceBenefit> builder)
        {
            builder.HasData(SeedData());
        }

        private List<InsuranceBenefit> SeedData()
        {
            string jsonContent = JsonReader.ReadJson("InsuranceBenefits.json");

            List<InsuranceBenefit>? insuranceBenefits = JsonSerializer.Deserialize<List<InsuranceBenefit>>(jsonContent);

            insuranceBenefits.ForEach(c => c.Id = Guid.NewGuid());

            return insuranceBenefits;
        }
    }
}
