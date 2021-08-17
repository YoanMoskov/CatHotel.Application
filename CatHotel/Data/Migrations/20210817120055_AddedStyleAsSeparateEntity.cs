using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class AddedStyleAsSeparateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Style",
                table: "Groomings",
                newName: "StyleId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Groomings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groomings_StyleId",
                table: "Groomings",
                column: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groomings_Styles_StyleId",
                table: "Groomings",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groomings_Styles_StyleId",
                table: "Groomings");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropIndex(
                name: "IX_Groomings_StyleId",
                table: "Groomings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Groomings");

            migrationBuilder.RenameColumn(
                name: "StyleId",
                table: "Groomings",
                newName: "Style");
        }
    }
}
