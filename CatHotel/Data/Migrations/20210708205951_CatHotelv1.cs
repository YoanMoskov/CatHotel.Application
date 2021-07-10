using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class CatHotelv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_AspNetUsers_UserId",
                table: "Cats");

            migrationBuilder.DropTable(
                name: "CatsSpecialNeeds");

            migrationBuilder.AddColumn<string>(
                name: "CatId",
                table: "SpecialNeeds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Cats",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialNeeds_CatId",
                table: "SpecialNeeds",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_AspNetUsers_UserId",
                table: "Cats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialNeeds_Cats_CatId",
                table: "SpecialNeeds",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_AspNetUsers_UserId",
                table: "Cats");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialNeeds_Cats_CatId",
                table: "SpecialNeeds");

            migrationBuilder.DropIndex(
                name: "IX_SpecialNeeds_CatId",
                table: "SpecialNeeds");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "SpecialNeeds");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Cats",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "CatsSpecialNeeds",
                columns: table => new
                {
                    CatId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpecialNeedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatsSpecialNeeds", x => new { x.CatId, x.SpecialNeedId });
                    table.ForeignKey(
                        name: "FK_CatsSpecialNeeds_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatsSpecialNeeds_SpecialNeeds_SpecialNeedId",
                        column: x => x.SpecialNeedId,
                        principalTable: "SpecialNeeds",
                        principalColumn: "SpecialNeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatsSpecialNeeds_SpecialNeedId",
                table: "CatsSpecialNeeds",
                column: "SpecialNeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_AspNetUsers_UserId",
                table: "Cats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
