using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tournaments.DAL.Implementation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMedCertificateRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    StudentWeight = table.Column<int>(type: "int", nullable: false),
                    StudentHeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentRequests_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "City", "Country", "Date", "IsMedCertificateRequired", "Name" },
                values: new object[,]
                {
                    { 1, "Moscow", "Russia", new DateTime(2021, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Moscow boxing championship" },
                    { 2, "Voronezh", "Russia", new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Voronezh Boxing League" },
                    { 3, "Gomel", "Belarus", new DateTime(2021, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "International Boxing Competition" },
                    { 4, "St. Petersburg", "Russia", new DateTime(2021, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "International boxing tournament - Cup of the Governor of St. Petersburg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRequests_TournamentId",
                table: "TournamentRequests",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentRequests");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
