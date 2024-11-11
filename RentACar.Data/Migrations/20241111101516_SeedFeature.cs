using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Feature name",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Feature name");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("003a7538-913e-4019-8ea3-0b4e45134a24"), "Луксозни" },
                    { new Guid("611810b8-924e-4e75-936f-18c027b8983e"), "SUV" },
                    { new Guid("8c709b6b-3c2a-4c2c-9668-43b9547f23d7"), "Ежедневни" },
                    { new Guid("95a07f61-f82e-42a2-932a-97253df6e5bf"), "Специални" },
                    { new Guid("c8cabedd-eaf1-4b29-9781-e49dc115f011"), "Спортни" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("109aef2a-541e-4722-8482-3ae464f58527"), "Подгрев на седалки" },
                    { new Guid("11c7a947-b15d-4a8f-848d-9272c8758a88"), "Адаптивен круиз контрол" },
                    { new Guid("3570df02-c4a0-4651-81c1-2da087fbd128"), "Подгряване на волана" },
                    { new Guid("50e88e14-9ab3-4715-9044-d0f09d18aba0"), "Камера за задно виждане" },
                    { new Guid("55727cf3-6a8f-49bd-a135-61b78f8f5434"), "USB портове за задните седалки" },
                    { new Guid("5f35a616-0751-42b4-a7b4-f3847db6b4d8"), "Wi-fi хотспот" },
                    { new Guid("5f6b5ab0-09e4-47b0-9153-75e9419aba5e"), "4x4 задвижване" },
                    { new Guid("a58ab910-8c32-49d1-b0e5-b20531e8bfaf"), "Режим за управление на сняг" },
                    { new Guid("a850447c-cc15-4d63-a8a4-a5420f843346"), "Лети джанти" },
                    { new Guid("b40e1e72-6a6c-49cf-87f4-152acb2b3865"), "Система за автоматично регулиране на дългите светлини" },
                    { new Guid("cede8358-3be0-4fe5-bfd9-5366e0e6c0b7"), "Двузонов климатроник" },
                    { new Guid("d3f5047e-be20-411c-831d-25e6821cc9ec"), "Автоматична скоростна кутия" },
                    { new Guid("d6452a15-be0c-4de0-8aab-8051a8fd6412"), "Климатик" },
                    { new Guid("da56aaf4-c11d-4e7e-a046-fdc0fd4b99e5"), "Навигационна система" },
                    { new Guid("e12aa088-edea-46fb-a342-800f17768377"), "Старт-стоп система" },
                    { new Guid("f569e47f-f312-493e-9fcd-53ceb21b8a51"), "Безключово палене" },
                    { new Guid("ffc530bb-61c6-4cdf-93d2-ca1fee7376a4"), "Дневни LED светлини" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("003a7538-913e-4019-8ea3-0b4e45134a24"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("611810b8-924e-4e75-936f-18c027b8983e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8c709b6b-3c2a-4c2c-9668-43b9547f23d7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("95a07f61-f82e-42a2-932a-97253df6e5bf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c8cabedd-eaf1-4b29-9781-e49dc115f011"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("109aef2a-541e-4722-8482-3ae464f58527"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("11c7a947-b15d-4a8f-848d-9272c8758a88"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("3570df02-c4a0-4651-81c1-2da087fbd128"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("50e88e14-9ab3-4715-9044-d0f09d18aba0"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("55727cf3-6a8f-49bd-a135-61b78f8f5434"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("5f35a616-0751-42b4-a7b4-f3847db6b4d8"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("5f6b5ab0-09e4-47b0-9153-75e9419aba5e"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("a58ab910-8c32-49d1-b0e5-b20531e8bfaf"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("a850447c-cc15-4d63-a8a4-a5420f843346"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("b40e1e72-6a6c-49cf-87f4-152acb2b3865"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("cede8358-3be0-4fe5-bfd9-5366e0e6c0b7"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("d3f5047e-be20-411c-831d-25e6821cc9ec"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("d6452a15-be0c-4de0-8aab-8051a8fd6412"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("da56aaf4-c11d-4e7e-a046-fdc0fd4b99e5"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e12aa088-edea-46fb-a342-800f17768377"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("f569e47f-f312-493e-9fcd-53ceb21b8a51"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("ffc530bb-61c6-4cdf-93d2-ca1fee7376a4"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Feature name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Feature name");

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
    }
}
