using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LOFC.Migrations
{
    public partial class Accounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Account_AccountId",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameColumn(
                name: "logIn",
                table: "Accounts",
                newName: "LogIn");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Accounts_AccountId",
                table: "Owners",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Accounts_AccountId",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameColumn(
                name: "LogIn",
                table: "Account",
                newName: "logIn");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Account_AccountId",
                table: "Owners",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
