using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class ChangedTournamentPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMedCertificateNecessary",
                table: "Tournaments",
                newName: "IsMedCertificateRequired");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "TournamentRequirements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMedCertificateRequired",
                table: "Tournaments",
                newName: "IsMedCertificateNecessary");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "TournamentRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
