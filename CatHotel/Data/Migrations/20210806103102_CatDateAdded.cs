using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class CatDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_Employees_EmployeeId",
                table: "Cats");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Cats_EmployeeId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Cats");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Cats",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Cats");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Cats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsVeterinarian = table.Column<bool>(type: "bit", nullable: false),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cats_EmployeeId",
                table: "Cats",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_Employees_EmployeeId",
                table: "Cats",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
