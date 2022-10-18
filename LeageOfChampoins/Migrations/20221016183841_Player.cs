using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOFC.Migrations
{
    public partial class Player : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_ClubId",
                table: "Players",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Clubs_ClubId",
                table: "Players",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Clubs_ClubId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_ClubId",
                table: "Players");
        }
    }
}
