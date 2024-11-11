using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarModelAddedImageUrlAndCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("637980e0-dafa-4492-bee2-4896c6e22e98"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("64598716-6e73-4830-9c32-18062327a025"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cedd0634-7ea3-443c-a40a-e020798bd754"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e2b004b7-d8e2-4c14-8487-445288d88baa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f85af879-ac09-439e-8b33-504cd41b2b29"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("48bda0ec-3a9d-4a49-afcd-2bdcdf589ef2"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("48beb215-f99b-4f34-b181-f29224494f9b"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("4ee0377d-e821-48aa-8811-a334d87b36ca"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("5b8d468f-b321-4910-b168-db4135fc2ded"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("6e8f1eaa-31a7-4b0a-816a-792e9cacc83b"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("755cf66a-6fe1-4ca2-a3e2-bad6bf51cd01"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("89050a2f-622a-43b8-b444-eef2d1b343b8"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("c7e5ef3a-2c38-4b42-bf4d-7f8254932f53"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("cd409ed9-eaf9-4827-b5e5-7d2d2c0657b9"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("d19e5b20-7a00-44b8-9c58-6e2153391da7"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("d7b73c25-33e3-4c7f-a164-009f86a22d39"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("d8434029-5cf6-47b8-8bee-92784cc2aff1"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e2db56a2-dddf-47e1-82e0-e9578aa8389c"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("eec8a24e-43a8-4c09-8d59-54257190b263"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("efbe1d4d-be12-4f65-9bba-c6e3fad0b728"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("f5e941e1-7e32-4d01-84ac-bfb46dc7686a"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("f70e94c5-4e85-45a5-9805-edc4ea608616"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("111f5874-9c53-4e81-804b-d3bf15cf4924"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("26df4951-6af1-4a2c-b45e-a87b37b4609e"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("4634145c-0b58-40e5-ab28-453dc7dbfcfb"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("4b133c49-58c6-46e3-ac64-76c15e394278"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("4f913d5f-56de-4b35-a578-0ebfd72f2ac2"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("6b6d085a-5eca-4c13-999c-acfa41c31fbb"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("7634e8de-6b07-4432-9711-7f6cc464b71c"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("8247b60f-6d1b-44f2-8d25-4b626c846289"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("92f0e642-8b86-4a60-9952-6f001462d193"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("a6f454a9-e1eb-4cbc-bbb8-b8d2d9431354"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("af6ea01c-ac3a-4f0e-ac5d-960fccf9e108"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("b56a255e-12a2-477b-b3e5-a45cffccd33b"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("d8a918c2-32df-48fe-887d-6a3bdd4140a4"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("ebbb0552-d7ab-4877-8457-f6ba29c74296"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("f4cc3f4e-758e-44d2-b51f-fbd248941104"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("3173028f-ec39-4056-b2f9-a6f2608fdd4d"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("3de2f94d-6abd-4b5c-86c0-8cd5a61c6d3a"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("40030c7e-7e40-42e1-bbee-34139d9fc383"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("461e3f2d-08eb-4ca4-b23d-bbfe9cc1c9d3"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("4a249ab9-522f-4c67-b2b5-4d332c94d691"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("67a2c81a-e6f3-4ed7-86ed-42f08d0daf0f"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("792c3757-2bc3-4b8a-8c49-4a164e1ad37d"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("7b54e3fa-b286-47a8-956a-ce732f7ce385"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("ce544101-8446-4841-9fb9-bb6a28f54a8d"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("d60fea38-a9e6-4c95-9d41-c034aea4e7bc"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("dc134064-d14c-4a5a-88c1-426ea1ddc247"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("dd6b81b1-7577-45e2-9399-ee37f9dcb5bc"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("f1e74d6d-6635-420f-a33b-f38451f036a9"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("f2c0a151-affd-45e7-86f1-a4af3c14591c"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("f2cd67b8-0be7-40d9-bd89-b789698c08e9"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Category of the car",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Car image url");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("23edbbdd-1138-4c0c-946f-052b397b4740"), "Специални" },
                    { new Guid("24abc7f3-5eab-49bd-bcfe-20fe1216aa0d"), "Луксозни" },
                    { new Guid("3409858e-09a0-4109-9190-8308d43424e1"), "SUV" },
                    { new Guid("75aaa9dc-9a55-435c-8d79-b9e1f14cce7f"), "Спортни" },
                    { new Guid("f325d7f2-4c3d-49d4-b29d-995431d33075"), "Ежедневни" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1040cd30-a7e0-46d5-8d41-6bcf7b0b6e13"), "Камера за задно виждане" },
                    { new Guid("22ddd41e-0084-41b9-b58e-05bfcb999578"), "USB портове за задните седалки" },
                    { new Guid("2526eda8-3bdb-4e92-ac50-aabe49c6ae80"), "Климатик" },
                    { new Guid("4390613c-0f09-4e61-b4c5-19f93e494294"), "4x4 задвижване" },
                    { new Guid("5416554f-1e8d-491d-8afe-72a3f7bd929f"), "Безключово палене" },
                    { new Guid("78869ecc-82f7-43bd-a7b8-64ccd8c58f27"), "Wi-fi хотспот" },
                    { new Guid("7a38506a-7756-4421-af2e-70854ba448b9"), "Навигационна система" },
                    { new Guid("7d9aa39a-6f5e-4ea0-bafc-fbdb444831f4"), "Двузонов климатроник" },
                    { new Guid("894b29d8-ecee-4846-895f-3bebeaf62901"), "Автоматична скоростна кутия" },
                    { new Guid("a334cd46-d62f-4d08-b57d-d9599782c1d3"), "Лети джанти" },
                    { new Guid("ae269eda-9d84-4b20-9408-e043b75e571e"), "Подгрев на седалки" },
                    { new Guid("cdbe437a-5e78-4a19-8f8a-b27359196438"), "Старт-стоп система" },
                    { new Guid("d184bfe9-454b-4e5e-ad7d-84ea4e64c74b"), "Дневни LED светлини" },
                    { new Guid("d37d82d1-1ad2-46f0-9260-4a222ad592fa"), "Режим за управление на сняг" },
                    { new Guid("ec98e689-5585-4617-b04f-7178e2541ac7"), "Подгряване на волана" },
                    { new Guid("ed7d1ce5-4adf-4a0e-99aa-3ba6d845bc5c"), "Система за автоматично регулиране на дългите светлини" },
                    { new Guid("fbc73450-879a-4d8e-a39b-76f7a6eb1c34"), "Адаптивен круиз контрол" }
                });

            migrationBuilder.InsertData(
                table: "InsuranceBenefits",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("27f71a9d-94c0-462f-90a0-7ef09b676aa1"), "Покритие на щети при вандализъм", 40.99m },
                    { new Guid("384fff0c-12ed-4000-a507-142e9496334d"), "Замяна на ключове при загуба", 25.49m },
                    { new Guid("3de12f22-752e-49eb-bcb9-6b51bd8d5422"), "Покритие на щети при природни бедствия", 45.25m },
                    { new Guid("5cff1185-84ca-4d37-880b-3647ad1babd7"), "Заместващ автомобил при авария", 50.0m },
                    { new Guid("772bd7e1-24c1-4b23-bc4b-187d9511abb8"), "Застраховка за лични вещи в автомобила", 45.89m },
                    { new Guid("94674f5e-16e7-4821-9bdf-a9b5b4fcab66"), "24/7 телефонна помощ", 15.0m },
                    { new Guid("9c751628-a296-4540-8e76-e4a33dff1af8"), "Покритие при повреда на стъклата", 22.89m },
                    { new Guid("aaf4a4db-43c3-4033-a7a6-6d7b6f1b0f4b"), "Покриване на разходи за транспорт", 18.99m },
                    { new Guid("afdb4386-8fd2-4f71-8849-1e7de9b1410d"), "Разходи за репатрак", 50.25m },
                    { new Guid("b3e93566-97d3-4eda-aaa7-dd0cc7af84fb"), "Техническа поддръжка на място", 35.0m },
                    { new Guid("b81e648a-ac47-41f3-823c-39aca3c5cdff"), "Медицинска застраховка за пътниците", 30.0m },
                    { new Guid("ba6d0c2b-a274-4dec-8bd5-3ebde5a68a80"), "Безплатна пътна помощ", 20.5m },
                    { new Guid("d2932afe-fbcf-409f-9a24-90124ad138c9"), "Покритие на щети при пожар", 35.75m },
                    { new Guid("dc9f80c5-8c79-429c-86a7-5f83bdd9dddb"), "Покритие при пътно-транспортно произшествие", 60.75m },
                    { new Guid("e7afa6f6-47d5-482d-b2b2-1d092bc3783e"), "Заместваща гума при повреда", 10.0m }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "PostalCode" },
                values: new object[,]
                {
                    { new Guid("3f287009-cb71-4e3a-8a95-38ad21bf1f84"), "Варна", 9000 },
                    { new Guid("5970741f-2533-4f92-b88a-6d82fd28fb22"), "Габрово", 5300 },
                    { new Guid("5c7769d1-d7d3-488a-8d46-425a60d180f2"), "Шумен", 9700 },
                    { new Guid("5d582be8-4780-4ba6-bfbc-7ea4fb40730b"), "Сливен", 8800 },
                    { new Guid("6b598db9-4c74-4d20-a96f-a9145a20e0dc"), "Стара Загора", 6000 },
                    { new Guid("87689ae6-b7e4-4816-a0ba-70dc544927ab"), "Пловдив", 4000 },
                    { new Guid("88c23054-f973-4a62-959f-ff4648fa822d"), "Добрич", 9300 },
                    { new Guid("8cfe9041-cbff-42dc-a3e7-3e493bfa6030"), "София", 1000 },
                    { new Guid("8deffb49-3791-4e1a-93fd-446762a13dda"), "Велико Търново", 5000 },
                    { new Guid("8edea8eb-1f4f-4098-aaaa-eac775ddc923"), "Плевен", 5800 },
                    { new Guid("99869746-cc00-4ad9-a814-76ceea03effd"), "Монтана", 3400 },
                    { new Guid("af630cf5-aa83-4edd-b48f-e5ae734ff1b9"), "Кърджали", 6600 },
                    { new Guid("c926651b-51e5-4811-8ec5-14ba1d817e56"), "Русе", 7000 },
                    { new Guid("e068fc58-e461-4b56-8592-dfbd5c753045"), "Благоевград", 2700 },
                    { new Guid("e6831e97-d057-4fe1-a9c0-9bf83c3b38fc"), "Бургас", 8000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("23edbbdd-1138-4c0c-946f-052b397b4740"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("24abc7f3-5eab-49bd-bcfe-20fe1216aa0d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3409858e-09a0-4109-9190-8308d43424e1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("75aaa9dc-9a55-435c-8d79-b9e1f14cce7f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f325d7f2-4c3d-49d4-b29d-995431d33075"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("1040cd30-a7e0-46d5-8d41-6bcf7b0b6e13"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("22ddd41e-0084-41b9-b58e-05bfcb999578"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("2526eda8-3bdb-4e92-ac50-aabe49c6ae80"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("4390613c-0f09-4e61-b4c5-19f93e494294"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("5416554f-1e8d-491d-8afe-72a3f7bd929f"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("78869ecc-82f7-43bd-a7b8-64ccd8c58f27"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("7a38506a-7756-4421-af2e-70854ba448b9"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("7d9aa39a-6f5e-4ea0-bafc-fbdb444831f4"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("894b29d8-ecee-4846-895f-3bebeaf62901"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("a334cd46-d62f-4d08-b57d-d9599782c1d3"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("ae269eda-9d84-4b20-9408-e043b75e571e"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("cdbe437a-5e78-4a19-8f8a-b27359196438"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("d184bfe9-454b-4e5e-ad7d-84ea4e64c74b"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("d37d82d1-1ad2-46f0-9260-4a222ad592fa"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("ec98e689-5585-4617-b04f-7178e2541ac7"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("ed7d1ce5-4adf-4a0e-99aa-3ba6d845bc5c"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("fbc73450-879a-4d8e-a39b-76f7a6eb1c34"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("27f71a9d-94c0-462f-90a0-7ef09b676aa1"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("384fff0c-12ed-4000-a507-142e9496334d"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("3de12f22-752e-49eb-bcb9-6b51bd8d5422"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("5cff1185-84ca-4d37-880b-3647ad1babd7"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("772bd7e1-24c1-4b23-bc4b-187d9511abb8"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("94674f5e-16e7-4821-9bdf-a9b5b4fcab66"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("9c751628-a296-4540-8e76-e4a33dff1af8"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("aaf4a4db-43c3-4033-a7a6-6d7b6f1b0f4b"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("afdb4386-8fd2-4f71-8849-1e7de9b1410d"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("b3e93566-97d3-4eda-aaa7-dd0cc7af84fb"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("b81e648a-ac47-41f3-823c-39aca3c5cdff"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("ba6d0c2b-a274-4dec-8bd5-3ebde5a68a80"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("d2932afe-fbcf-409f-9a24-90124ad138c9"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("dc9f80c5-8c79-429c-86a7-5f83bdd9dddb"));

            migrationBuilder.DeleteData(
                table: "InsuranceBenefits",
                keyColumn: "Id",
                keyValue: new Guid("e7afa6f6-47d5-482d-b2b2-1d092bc3783e"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("3f287009-cb71-4e3a-8a95-38ad21bf1f84"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("5970741f-2533-4f92-b88a-6d82fd28fb22"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("5c7769d1-d7d3-488a-8d46-425a60d180f2"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("5d582be8-4780-4ba6-bfbc-7ea4fb40730b"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("6b598db9-4c74-4d20-a96f-a9145a20e0dc"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("87689ae6-b7e4-4816-a0ba-70dc544927ab"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("88c23054-f973-4a62-959f-ff4648fa822d"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("8cfe9041-cbff-42dc-a3e7-3e493bfa6030"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("8deffb49-3791-4e1a-93fd-446762a13dda"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("8edea8eb-1f4f-4098-aaaa-eac775ddc923"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("99869746-cc00-4ad9-a814-76ceea03effd"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("af630cf5-aa83-4edd-b48f-e5ae734ff1b9"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("c926651b-51e5-4811-8ec5-14ba1d817e56"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("e068fc58-e461-4b56-8592-dfbd5c753045"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("e6831e97-d057-4fe1-a9c0-9bf83c3b38fc"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cars");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Category of the car");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("637980e0-dafa-4492-bee2-4896c6e22e98"), "Спортни" },
                    { new Guid("64598716-6e73-4830-9c32-18062327a025"), "Специални" },
                    { new Guid("cedd0634-7ea3-443c-a40a-e020798bd754"), "Луксозни" },
                    { new Guid("e2b004b7-d8e2-4c14-8487-445288d88baa"), "SUV" },
                    { new Guid("f85af879-ac09-439e-8b33-504cd41b2b29"), "Ежедневни" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("48bda0ec-3a9d-4a49-afcd-2bdcdf589ef2"), "Подгряване на волана" },
                    { new Guid("48beb215-f99b-4f34-b181-f29224494f9b"), "Wi-fi хотспот" },
                    { new Guid("4ee0377d-e821-48aa-8811-a334d87b36ca"), "USB портове за задните седалки" },
                    { new Guid("5b8d468f-b321-4910-b168-db4135fc2ded"), "Климатик" },
                    { new Guid("6e8f1eaa-31a7-4b0a-816a-792e9cacc83b"), "Навигационна система" },
                    { new Guid("755cf66a-6fe1-4ca2-a3e2-bad6bf51cd01"), "Лети джанти" },
                    { new Guid("89050a2f-622a-43b8-b444-eef2d1b343b8"), "Адаптивен круиз контрол" },
                    { new Guid("c7e5ef3a-2c38-4b42-bf4d-7f8254932f53"), "Двузонов климатроник" },
                    { new Guid("cd409ed9-eaf9-4827-b5e5-7d2d2c0657b9"), "4x4 задвижване" },
                    { new Guid("d19e5b20-7a00-44b8-9c58-6e2153391da7"), "Режим за управление на сняг" },
                    { new Guid("d7b73c25-33e3-4c7f-a164-009f86a22d39"), "Безключово палене" },
                    { new Guid("d8434029-5cf6-47b8-8bee-92784cc2aff1"), "Дневни LED светлини" },
                    { new Guid("e2db56a2-dddf-47e1-82e0-e9578aa8389c"), "Подгрев на седалки" },
                    { new Guid("eec8a24e-43a8-4c09-8d59-54257190b263"), "Система за автоматично регулиране на дългите светлини" },
                    { new Guid("efbe1d4d-be12-4f65-9bba-c6e3fad0b728"), "Камера за задно виждане" },
                    { new Guid("f5e941e1-7e32-4d01-84ac-bfb46dc7686a"), "Старт-стоп система" },
                    { new Guid("f70e94c5-4e85-45a5-9805-edc4ea608616"), "Автоматична скоростна кутия" }
                });

            migrationBuilder.InsertData(
                table: "InsuranceBenefits",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("111f5874-9c53-4e81-804b-d3bf15cf4924"), "Техническа поддръжка на място", 35.0m },
                    { new Guid("26df4951-6af1-4a2c-b45e-a87b37b4609e"), "Покритие при повреда на стъклата", 22.89m },
                    { new Guid("4634145c-0b58-40e5-ab28-453dc7dbfcfb"), "Покритие на щети при пожар", 35.75m },
                    { new Guid("4b133c49-58c6-46e3-ac64-76c15e394278"), "Заместваща гума при повреда", 10.0m },
                    { new Guid("4f913d5f-56de-4b35-a578-0ebfd72f2ac2"), "Покриване на разходи за транспорт", 18.99m },
                    { new Guid("6b6d085a-5eca-4c13-999c-acfa41c31fbb"), "Заместващ автомобил при авария", 50.0m },
                    { new Guid("7634e8de-6b07-4432-9711-7f6cc464b71c"), "Медицинска застраховка за пътниците", 30.0m },
                    { new Guid("8247b60f-6d1b-44f2-8d25-4b626c846289"), "Покритие на щети при природни бедствия", 45.25m },
                    { new Guid("92f0e642-8b86-4a60-9952-6f001462d193"), "Застраховка за лични вещи в автомобила", 45.89m },
                    { new Guid("a6f454a9-e1eb-4cbc-bbb8-b8d2d9431354"), "Разходи за репатрак", 50.25m },
                    { new Guid("af6ea01c-ac3a-4f0e-ac5d-960fccf9e108"), "Безплатна пътна помощ", 20.5m },
                    { new Guid("b56a255e-12a2-477b-b3e5-a45cffccd33b"), "Покритие при пътно-транспортно произшествие", 60.75m },
                    { new Guid("d8a918c2-32df-48fe-887d-6a3bdd4140a4"), "Покритие на щети при вандализъм", 40.99m },
                    { new Guid("ebbb0552-d7ab-4877-8457-f6ba29c74296"), "Замяна на ключове при загуба", 25.49m },
                    { new Guid("f4cc3f4e-758e-44d2-b51f-fbd248941104"), "24/7 телефонна помощ", 15.0m }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "PostalCode" },
                values: new object[,]
                {
                    { new Guid("3173028f-ec39-4056-b2f9-a6f2608fdd4d"), "Габрово", 5300 },
                    { new Guid("3de2f94d-6abd-4b5c-86c0-8cd5a61c6d3a"), "Бургас", 8000 },
                    { new Guid("40030c7e-7e40-42e1-bbee-34139d9fc383"), "Добрич", 9300 },
                    { new Guid("461e3f2d-08eb-4ca4-b23d-bbfe9cc1c9d3"), "Благоевград", 2700 },
                    { new Guid("4a249ab9-522f-4c67-b2b5-4d332c94d691"), "Плевен", 5800 },
                    { new Guid("67a2c81a-e6f3-4ed7-86ed-42f08d0daf0f"), "Велико Търново", 5000 },
                    { new Guid("792c3757-2bc3-4b8a-8c49-4a164e1ad37d"), "Монтана", 3400 },
                    { new Guid("7b54e3fa-b286-47a8-956a-ce732f7ce385"), "Шумен", 9700 },
                    { new Guid("ce544101-8446-4841-9fb9-bb6a28f54a8d"), "Пловдив", 4000 },
                    { new Guid("d60fea38-a9e6-4c95-9d41-c034aea4e7bc"), "София", 1000 },
                    { new Guid("dc134064-d14c-4a5a-88c1-426ea1ddc247"), "Русе", 7000 },
                    { new Guid("dd6b81b1-7577-45e2-9399-ee37f9dcb5bc"), "Кърджали", 6600 },
                    { new Guid("f1e74d6d-6635-420f-a33b-f38451f036a9"), "Варна", 9000 },
                    { new Guid("f2c0a151-affd-45e7-86f1-a4af3c14591c"), "Стара Загора", 6000 },
                    { new Guid("f2cd67b8-0be7-40d9-bd89-b789698c08e9"), "Сливен", 8800 }
                });
        }
    }
}
