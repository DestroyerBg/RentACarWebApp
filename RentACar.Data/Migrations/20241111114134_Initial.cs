using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "User's first name."),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "User's last name"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "User's birthdate."),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Category name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Feature name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceBenefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Issurance benefit name"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceBenefits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "City name"),
                    PostalCode = table.Column<int>(type: "int", nullable: false, comment: "City's postal code")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "E.G. BMW, Mercedes or etc"),
                    Model = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "E.G. model number like E36, E60 or w211"),
                    HorsePower = table.Column<int>(type: "int", nullable: false, comment: "Car's horsepower"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false, comment: "Car's registration number"),
                    YearOfManufacture = table.Column<int>(type: "int", nullable: false, comment: "Year when car was produced"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Car's location"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is the entity deleted?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarsFeatures",
                columns: table => new
                {
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsFeatures", x => new { x.CarId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_CarsFeatures_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarsFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "This is customer id"),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "This is car id"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "When the reservation begins"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "When the reservation ends"),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Total price for the reservation"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Is the entity deleted?"),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerFeedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the customer"),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the reservation"),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the car"),
                    Rating = table.Column<double>(type: "float", nullable: false, comment: "Stars given from user"),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false, comment: "Feedback description"),
                    DateOfSubmission = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date when comment was post")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerFeedbacks_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerFeedbacks_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerFeedbacks_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the reservation for which is the issurance"),
                    InsuranceProvider = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Insurance provider name"),
                    InsuranceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "This is a sum of the price from all insurance benefits"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "When the issurance expirates")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurances_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceInsuranceBenefit",
                columns: table => new
                {
                    InsuranceBenefitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsurancesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceInsuranceBenefit", x => new { x.InsuranceBenefitsId, x.InsurancesId });
                    table.ForeignKey(
                        name: "FK_InsuranceInsuranceBenefit_InsuranceBenefits_InsuranceBenefitsId",
                        column: x => x.InsuranceBenefitsId,
                        principalTable: "InsuranceBenefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceInsuranceBenefit_Insurances_InsurancesId",
                        column: x => x.InsurancesId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_LocationId",
                table: "Cars",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsFeatures_FeatureId",
                table: "CarsFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_CarId",
                table: "CustomerFeedbacks",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_CustomerId",
                table: "CustomerFeedbacks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_ReservationId",
                table: "CustomerFeedbacks",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceInsuranceBenefit_InsurancesId",
                table: "InsuranceInsuranceBenefit",
                column: "InsurancesId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_ReservationId",
                table: "Insurances",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LocationId",
                table: "Reservations",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarsFeatures");

            migrationBuilder.DropTable(
                name: "CustomerFeedbacks");

            migrationBuilder.DropTable(
                name: "InsuranceInsuranceBenefit");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "InsuranceBenefits");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
