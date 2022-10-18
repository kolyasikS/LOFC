using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOFC.Migrations
{
    public partial class Club : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "posInLeague",
                table: "Clubs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "posInLeague",
                table: "Clubs");
        }
    }
}
