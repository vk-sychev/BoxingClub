using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class ChangedToCascadeOnTournamentRequrements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {;

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRequirements_Tournaments_TournamentId",
                table: "TournamentRequirements");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "TournamentRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRequirements_Tournaments_TournamentId",
                table: "TournamentRequirements",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRequirements_Tournaments_TournamentId",
                table: "TournamentRequirements");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentId",
                table: "TournamentRequirements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRequirements_Tournaments_TournamentId",
                table: "TournamentRequirements",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
