using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1b916af1-b322-4ea2-9d8c-c8fa7055f6b3"), "Ежедневни" },
                    { new Guid("85a1fbb2-96e6-4ac9-a763-79ec1e684e24"), "SUV" },
                    { new Guid("8f49eae6-1329-41bb-a2e0-78b11cf9c50d"), "Спортни" },
                    { new Guid("c066c085-2357-4a64-885a-c19d311c08a8"), "Специални" },
                    { new Guid("f6924f6b-1d12-4647-a2a8-64c52158d075"), "Луксозни" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("012d8597-b185-4506-8ea6-0ce343ec6b1f"), "Дневни LED светлини" },
                    { new Guid("0dfebc3a-bce6-4f35-87dd-0c9e2bc42d88"), "Система за автоматично регулиране на дългите светлини" },
                    { new Guid("2048d92c-5d27-44b4-bcc0-fc9e76e1fbc2"), "Автоматична скоростна кутия" },
                    { new Guid("2a298718-05f9-41ee-821b-5d90baf6dcea"), "Wi-fi хотспот" },
                    { new Guid("30b20046-0f0a-4f6c-ab0f-9ab9ede1b610"), "Подгрев на седалки" },
                    { new Guid("42b60b5e-0b91-4943-8cfd-88420eae105a"), "Безключово палене" },
                    { new Guid("4fc64f93-3942-4d45-a1b5-2b54e302149b"), "USB портове за задните седалки" },
                    { new Guid("535e913d-0f6e-4318-b128-a6ecd04b4e74"), "Двузонов климатроник" },
                    { new Guid("6db368f5-8cef-4a1b-b63b-4d4078023aff"), "4x4 задвижване" },
                    { new Guid("7724a1e4-5336-4f56-8f08-b241c162a6c3"), "Старт-стоп система" },
                    { new Guid("7a89ef81-86a3-4aca-9063-6b0d86574a69"), "Режим за управление на сняг" },
                    { new Guid("a2eb42c9-a4ab-4a1c-bff0-421202b8f1cd"), "Камера за задно виждане" },
                    { new Guid("b2cbf9f6-82c7-442d-8767-08f162fd9074"), "Подгряване на волана" },
                    { new Guid("bd334a6a-059e-4c83-a013-36ed8906973d"), "Лети джанти" },
                    { new Guid("e3febe51-6253-4143-8962-1a9574c19419"), "Климатик" },
                    { new Guid("e4eaae84-ddad-43c4-b70a-cb6e164dc3aa"), "Навигационна система" },
                    { new Guid("f3a6bfe8-28db-44ee-b3ab-bd37a34cf457"), "Адаптивен круиз контрол" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "PostalCode" },
                values: new object[,]
                {
                    { new Guid("046017b3-55d5-407e-a23b-52250ed25774"), "Велико Търново", 5000 },
                    { new Guid("1022c0eb-fbea-44ac-9261-f770a8dec66b"), "Монтана", 3400 },
                    { new Guid("2eb6f329-76cc-4f3d-8bfd-9ff3a1cd2fdd"), "Шумен", 9700 },
                    { new Guid("30b8ce88-8536-4e66-8561-2e0d7a4ada53"), "Сливен", 8800 },
                    { new Guid("3526a8a5-4a3b-4e91-ac9a-dae50b216e46"), "Габрово", 5300 },
                    { new Guid("5e8bd010-6bff-4f05-afbc-9cee1509efad"), "Кърджали", 6600 },
                    { new Guid("70e08947-b59d-49a9-ab99-3247cb571c84"), "София", 1000 },
                    { new Guid("90e51e92-fa42-45cb-b9b7-ab5f9c3f38cb"), "Русе", 7000 },
                    { new Guid("97787cca-d552-4d52-8f97-39a4213dfed7"), "Варна", 9000 },
                    { new Guid("abd17c5a-1114-4177-bd63-36970b5c2236"), "Пловдив", 4000 },
                    { new Guid("c9e95cef-5b92-48a4-84bc-6d26c2ac3dfb"), "Благоевград", 2700 },
                    { new Guid("cb1b3107-8b1f-4a9e-a15d-ed2311d461ef"), "Стара Загора", 6000 },
                    { new Guid("ce6a1edf-e6aa-4bf5-9049-ee583eeb9130"), "Добрич", 9300 },
                    { new Guid("e586990c-7ff0-41ef-8c57-77053e984b03"), "Бургас", 8000 },
                    { new Guid("ef1ce795-ba28-4653-a0f6-10bb462fadc2"), "Плевен", 5800 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1b916af1-b322-4ea2-9d8c-c8fa7055f6b3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("85a1fbb2-96e6-4ac9-a763-79ec1e684e24"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f49eae6-1329-41bb-a2e0-78b11cf9c50d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c066c085-2357-4a64-885a-c19d311c08a8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f6924f6b-1d12-4647-a2a8-64c52158d075"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("012d8597-b185-4506-8ea6-0ce343ec6b1f"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("0dfebc3a-bce6-4f35-87dd-0c9e2bc42d88"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("2048d92c-5d27-44b4-bcc0-fc9e76e1fbc2"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("2a298718-05f9-41ee-821b-5d90baf6dcea"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("30b20046-0f0a-4f6c-ab0f-9ab9ede1b610"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("42b60b5e-0b91-4943-8cfd-88420eae105a"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("4fc64f93-3942-4d45-a1b5-2b54e302149b"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("535e913d-0f6e-4318-b128-a6ecd04b4e74"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("6db368f5-8cef-4a1b-b63b-4d4078023aff"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("7724a1e4-5336-4f56-8f08-b241c162a6c3"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("7a89ef81-86a3-4aca-9063-6b0d86574a69"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("a2eb42c9-a4ab-4a1c-bff0-421202b8f1cd"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("b2cbf9f6-82c7-442d-8767-08f162fd9074"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("bd334a6a-059e-4c83-a013-36ed8906973d"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e3febe51-6253-4143-8962-1a9574c19419"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e4eaae84-ddad-43c4-b70a-cb6e164dc3aa"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("f3a6bfe8-28db-44ee-b3ab-bd37a34cf457"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("046017b3-55d5-407e-a23b-52250ed25774"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("1022c0eb-fbea-44ac-9261-f770a8dec66b"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("2eb6f329-76cc-4f3d-8bfd-9ff3a1cd2fdd"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("30b8ce88-8536-4e66-8561-2e0d7a4ada53"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("3526a8a5-4a3b-4e91-ac9a-dae50b216e46"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("5e8bd010-6bff-4f05-afbc-9cee1509efad"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("70e08947-b59d-49a9-ab99-3247cb571c84"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("90e51e92-fa42-45cb-b9b7-ab5f9c3f38cb"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("97787cca-d552-4d52-8f97-39a4213dfed7"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("abd17c5a-1114-4177-bd63-36970b5c2236"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("c9e95cef-5b92-48a4-84bc-6d26c2ac3dfb"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("cb1b3107-8b1f-4a9e-a15d-ed2311d461ef"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("ce6a1edf-e6aa-4bf5-9049-ee583eeb9130"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("e586990c-7ff0-41ef-8c57-77053e984b03"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("ef1ce795-ba28-4653-a0f6-10bb462fadc2"));

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
    }
}
