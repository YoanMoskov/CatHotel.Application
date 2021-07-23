using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class CatHotelv18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPaid",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CatSize",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroomingId",
                table: "Cats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grooming",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DescribePreferredStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grooming", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grooming_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cats_GroomingId",
                table: "Cats",
                column: "GroomingId");

            migrationBuilder.CreateIndex(
                name: "IX_Grooming_PaymentId",
                table: "Grooming",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_Grooming_GroomingId",
                table: "Cats",
                column: "GroomingId",
                principalTable: "Grooming",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_Grooming_GroomingId",
                table: "Cats");

            migrationBuilder.DropTable(
                name: "Grooming");

            migrationBuilder.DropIndex(
                name: "IX_Cats_GroomingId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "isPaid",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CatSize",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "GroomingId",
                table: "Cats");
        }
    }
}
