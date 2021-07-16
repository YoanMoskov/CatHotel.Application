using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class CatHotelv16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Cats",
                newName: "AdditionalInformation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalInformation",
                table: "Cats",
                newName: "Description");
        }
    }
}
