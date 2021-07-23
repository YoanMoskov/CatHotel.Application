using Microsoft.EntityFrameworkCore.Migrations;

namespace CatHotel.Data.Migrations
{
    public partial class CatHotelv21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_Reservations_ReservationId",
                table: "Cats");

            migrationBuilder.DropIndex(
                name: "IX_Cats_ReservationId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Cats");

            migrationBuilder.CreateTable(
                name: "CatReservation",
                columns: table => new
                {
                    CatsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReservationsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatReservation", x => new { x.CatsId, x.ReservationsId });
                    table.ForeignKey(
                        name: "FK_CatReservation_Cats_CatsId",
                        column: x => x.CatsId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatReservation_Reservations_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatReservation_ReservationsId",
                table: "CatReservation",
                column: "ReservationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatReservation");

            migrationBuilder.AddColumn<string>(
                name: "ReservationId",
                table: "Cats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cats_ReservationId",
                table: "Cats",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_Reservations_ReservationId",
                table: "Cats",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
