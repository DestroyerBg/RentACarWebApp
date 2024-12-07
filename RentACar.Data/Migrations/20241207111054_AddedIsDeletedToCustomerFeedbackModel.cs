using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedToCustomerFeedbackModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFeedbacks_Reservations_ReservationId",
                table: "CustomerFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_CustomerFeedbacks_ReservationId",
                table: "CustomerFeedbacks");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "CustomerFeedbacks");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "CustomerFeedbacks",
                type: "int",
                nullable: false,
                comment: "Stars given from user",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "Stars given from user");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CustomerFeedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CustomerFeedbacks");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "CustomerFeedbacks",
                type: "float",
                nullable: false,
                comment: "Stars given from user",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Stars given from user");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "CustomerFeedbacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of the reservation");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_ReservationId",
                table: "CustomerFeedbacks",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFeedbacks_Reservations_ReservationId",
                table: "CustomerFeedbacks",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
