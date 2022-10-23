using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOFC.Migrations
{
    public partial class Clubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Clubs_ClubId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_ClubId",
                table: "Owners");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_ClubId",
                table: "Owners",
                column: "ClubId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Clubs_ClubId",
                table: "Owners",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Clubs_ClubId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_ClubId",
                table: "Owners");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_ClubId",
                table: "Owners",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Clubs_ClubId",
                table: "Owners",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }
    }
}
