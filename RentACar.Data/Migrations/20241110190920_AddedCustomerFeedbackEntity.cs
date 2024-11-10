using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerFeedbackEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "InsuranceId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerFeedbacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "InsuranceId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
