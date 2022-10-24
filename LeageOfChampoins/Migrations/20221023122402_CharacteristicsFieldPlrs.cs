using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LOFC.Migrations
{
    public partial class CharacteristicsFieldPlrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacteristicsFieldPlrs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Dribbling = table.Column<int>(type: "integer", nullable: false),
                    Shooting = table.Column<int>(type: "integer", nullable: false),
                    Defending = table.Column<int>(type: "integer", nullable: false),
                    Pass = table.Column<int>(type: "integer", nullable: false),
                    Physics = table.Column<int>(type: "integer", nullable: false),
                    playerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicsFieldPlrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicsFieldPlrs_Players_playerId",
                        column: x => x.playerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicsGoalkeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Diving = table.Column<int>(type: "integer", nullable: false),
                    Handling = table.Column<int>(type: "integer", nullable: false),
                    Positioning = table.Column<int>(type: "integer", nullable: false),
                    Kicking = table.Column<int>(type: "integer", nullable: false),
                    Reflexes = table.Column<int>(type: "integer", nullable: false),
                    playerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicsGoalkeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicsGoalkeepers_Players_playerId",
                        column: x => x.playerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicsFieldPlrs_playerId",
                table: "CharacteristicsFieldPlrs",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicsGoalkeepers_playerId",
                table: "CharacteristicsGoalkeepers",
                column: "playerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacteristicsFieldPlrs");

            migrationBuilder.DropTable(
                name: "CharacteristicsGoalkeepers");
        }
    }
}
