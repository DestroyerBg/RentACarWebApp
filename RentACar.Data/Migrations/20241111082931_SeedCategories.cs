using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3d98c697-402d-4524-bb88-2f199e854095"), "Специални" },
                    { new Guid("5ef21ae2-fcb9-4ebf-8816-4dcfe9ad9822"), "Спортни" },
                    { new Guid("60b6a922-72af-478b-b9c7-99a74a93afd5"), "SUV" },
                    { new Guid("8e788f19-496e-4681-a422-d3de25f37221"), "Ежедневни" },
                    { new Guid("9716fed9-924b-4418-94b3-aed7d2fec23e"), "Луксозни" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3d98c697-402d-4524-bb88-2f199e854095"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5ef21ae2-fcb9-4ebf-8816-4dcfe9ad9822"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("60b6a922-72af-478b-b9c7-99a74a93afd5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8e788f19-496e-4681-a422-d3de25f37221"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9716fed9-924b-4418-94b3-aed7d2fec23e"));
        }
    }
}
