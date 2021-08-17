using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class AdditionalFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groomings_Payments_PaymentId",
                table: "Groomings");

            migrationBuilder.DropIndex(
                name: "IX_Groomings_PaymentId",
                table: "Groomings");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Groomings");

            migrationBuilder.AddColumn<DateTime>(
                name: "Appointment",
                table: "Groomings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Appointment",
                table: "Groomings");

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Groomings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groomings_PaymentId",
                table: "Groomings",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groomings_Payments_PaymentId",
                table: "Groomings",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
