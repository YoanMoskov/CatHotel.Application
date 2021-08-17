using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class AddedStyleToGrooming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescribePreferredStyle",
                table: "Groomings");

            migrationBuilder.DropColumn(
                name: "CatSize",
                table: "Cats");

            migrationBuilder.AddColumn<int>(
                name: "Style",
                table: "Groomings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Style",
                table: "Groomings");

            migrationBuilder.AddColumn<string>(
                name: "DescribePreferredStyle",
                table: "Groomings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatSize",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
