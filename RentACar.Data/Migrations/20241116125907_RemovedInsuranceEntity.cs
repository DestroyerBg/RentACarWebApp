using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedInsuranceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceInsuranceBenefit");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "InsuranceBenefits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceBenefits_ReservationId",
                table: "InsuranceBenefits",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceBenefits_Reservations_ReservationId",
                table: "InsuranceBenefits",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceBenefits_Reservations_ReservationId",
                table: "InsuranceBenefits");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceBenefits_ReservationId",
                table: "InsuranceBenefits");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "InsuranceBenefits");

            migrationBuilder.AddColumn<Guid>(
                name: "InsuranceId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the reservation for which is the issurance"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "When the issurance expirates"),
                    InsuranceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "This is a sum of the price from all insurance benefits"),
                    InsuranceProvider = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Insurance provider name")
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

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceInsuranceBenefit_InsurancesId",
                table: "InsuranceInsuranceBenefit",
                column: "InsurancesId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_ReservationId",
                table: "Insurances",
                column: "ReservationId",
                unique: true);
        }
    }
}
