using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInsuranceAndInsuranceBenefit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InsuranceId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "InsuranceBenefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Issurance benefit name"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceBenefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceBenefits_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceBenefits_InsuranceId",
                table: "InsuranceBenefits",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_ReservationId",
                table: "Insurances",
                column: "ReservationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceBenefits");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Reservations");
        }
    }
}
