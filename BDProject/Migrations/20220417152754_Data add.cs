using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDProject.Migrations
{
    public partial class Dataadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_Team1Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_Team2Id",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_Team1Id",
                table: "Games",
                column: "Team1Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_Team2Id",
                table: "Games",
                column: "Team2Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_Team1Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_Team2Id",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_Team1Id",
                table: "Games",
                column: "Team1Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_Team2Id",
                table: "Games",
                column: "Team2Id",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
