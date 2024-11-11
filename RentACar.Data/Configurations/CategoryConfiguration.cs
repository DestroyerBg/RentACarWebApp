using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedData());
        }

        private List<Category> SeedData()
        {
            string solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            string filePath = Path.Combine(solutionRoot, "RentACar.Data", "Seeder", "JSON", "Categories.json");

            string jsonContent = File.ReadAllText(filePath);

            List<Category>? categories = JsonSerializer.Deserialize<List<Category>>(jsonContent);

            categories.ForEach(c => c.Id = Guid.NewGuid());

            return categories;
        }
    }
}
