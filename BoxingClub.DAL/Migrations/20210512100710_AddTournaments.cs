using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class AddTournaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartAge = table.Column<int>(type: "int", nullable: false),
                    EndAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMedCertificateNecessary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartWeight = table.Column<int>(type: "int", nullable: false),
                    EndWeight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeCategoryId = table.Column<int>(type: "int", nullable: false),
                    WeightCategoryId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AgeCategories_AgeCategoryId",
                        column: x => x.AgeCategoryId,
                        principalTable: "AgeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_WeightCategories_WeightCategoryId",
                        column: x => x.WeightCategoryId,
                        principalTable: "WeightCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AgeCategories",
                columns: new[] { "Id", "EndAge", "Name", "StartAge" },
                values: new object[,]
                {
                    { 1, 16, "15-16 years old", 15 },
                    { 2, 18, "Juniors", 17 },
                    { 3, 40, "Adults", 19 }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "City", "Country", "Date", "IsMedCertificateNecessary", "Name" },
                values: new object[,]
                {
                    { 4, "St. Petersburg", "Russia", new DateTime(2021, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "International boxing tournament - Cup of the Governor of St. Petersburg" },
                    { 1, "Moscow", "Russia", new DateTime(2021, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Moscow junior boxing championship" },
                    { 2, "Voronezh", "Russia", new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Voronezh Boxing League" },
                    { 3, "Gomel", "Belarus", new DateTime(2021, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "International Women's Boxing Competition" }
                });

            migrationBuilder.InsertData(
                table: "WeightCategories",
                columns: new[] { "Id", "EndWeight", "Name", "StartWeight" },
                values: new object[,]
                {
                    { 9, 75, "Middleweight", 70 },
                    { 10, null, "Heavyweight", 80 },
                    { 5, 60, "Lightweight", 57 },
                    { 7, null, "Heavyweight", 81 },
                    { 4, null, "Super heavyweight", 91 },
                    { 2, 75, "Middleweight", 69 },
                    { 1, 60, "Lightweight", 56 },
                    { 3, 91, "Heavyweight", 81 },
                    { 8, 63, "Lightweight", 60 },
                    { 6, 75, "Middleweight", 69 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[,]
                {
                    { 3, 2, 1, "Heavyweight - Boys-juniors", 1, 3 },
                    { 7, 1, 0, "Lightweight - Girls 15-16 years old", 2, 8 },
                    { 4, 1, 1, "Lightweight - Boys 15-16 years old", 2, 8 },
                    { 9, 1, 0, "Heavyweight - Girls 15-16 years old", 2, 10 },
                    { 6, 1, 1, "Heavyweight - Boys 15-16 years old", 2, 10 },
                    { 21, 3, 0, "Miidleweight - Females", 3, 6 },
                    { 20, 3, 0, "Lightweight - Females", 3, 5 },
                    { 22, 3, 0, "Heavyweight - Females", 3, 7 },
                    { 19, 3, 1, "Super heavyweight - Males", 4, 4 },
                    { 17, 3, 1, "Miidleweight - Males", 4, 2 },
                    { 14, 2, 0, "Middleweight - Girls-juniors", 2, 2 },
                    { 11, 2, 1, "Middleweight - Boys-juniors", 2, 2 },
                    { 2, 2, 1, "Middleweight - Boys-juniors", 1, 2 },
                    { 16, 3, 1, "Lightweight - Males", 4, 1 },
                    { 13, 2, 0, "Lightweight - Girls-juniors", 2, 1 },
                    { 10, 2, 1, "Lightweight - Boys-juniors", 2, 1 },
                    { 1, 2, 1, "Lightweight - Boys-juniors", 1, 1 },
                    { 18, 3, 1, "Heavyweight - Males", 4, 3 },
                    { 15, 2, 0, "Heavyweight - Girls-juniors", 2, 3 },
                    { 12, 2, 1, "Heavyweight - Boys-juniors", 2, 3 },
                    { 5, 1, 1, "Middlweight - Boys 15-16 years old", 2, 9 },
                    { 8, 1, 0, "Middleweight - Girls 15-16 years old", 2, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AgeCategoryId",
                table: "Categories",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TournamentId",
                table: "Categories",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_WeightCategoryId",
                table: "Categories",
                column: "WeightCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AgeCategories");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "WeightCategories");
        }
    }
}
