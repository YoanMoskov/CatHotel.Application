using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class AddGrooming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_Grooming_GroomingId",
                table: "Cats");

            migrationBuilder.DropForeignKey(
                name: "FK_Grooming_Payments_PaymentId",
                table: "Grooming");

            migrationBuilder.DropIndex(
                name: "IX_Cats_GroomingId",
                table: "Cats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grooming",
                table: "Grooming");

            migrationBuilder.DropColumn(
                name: "GroomingId",
                table: "Cats");

            migrationBuilder.RenameTable(
                name: "Grooming",
                newName: "Groomings");

            migrationBuilder.RenameIndex(
                name: "IX_Grooming_PaymentId",
                table: "Groomings",
                newName: "IX_Groomings_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groomings",
                table: "Groomings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CatsGroomings",
                columns: table => new
                {
                    CatId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroomingId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatsGroomings", x => new { x.CatId, x.GroomingId });
                    table.ForeignKey(
                        name: "FK_CatsGroomings_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatsGroomings_Groomings_GroomingId",
                        column: x => x.GroomingId,
                        principalTable: "Groomings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatsGroomings_GroomingId",
                table: "CatsGroomings",
                column: "GroomingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groomings_Payments_PaymentId",
                table: "Groomings",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groomings_Payments_PaymentId",
                table: "Groomings");

            migrationBuilder.DropTable(
                name: "CatsGroomings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groomings",
                table: "Groomings");

            migrationBuilder.RenameTable(
                name: "Groomings",
                newName: "Grooming");

            migrationBuilder.RenameIndex(
                name: "IX_Groomings_PaymentId",
                table: "Grooming",
                newName: "IX_Grooming_PaymentId");

            migrationBuilder.AddColumn<string>(
                name: "GroomingId",
                table: "Cats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grooming",
                table: "Grooming",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_GroomingId",
                table: "Cats",
                column: "GroomingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_Grooming_GroomingId",
                table: "Cats",
                column: "GroomingId",
                principalTable: "Grooming",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grooming_Payments_PaymentId",
                table: "Grooming",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
