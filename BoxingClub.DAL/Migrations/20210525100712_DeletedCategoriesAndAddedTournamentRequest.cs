using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class DeletedCategoriesAndAddedTournamentRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
/*            migrationBuilder.DropForeignKey("FK_Students_Categories_CategoryId", "Students");
            migrationBuilder.DropForeignKey("FK_Students_Tournaments_TournamentId", "Students");*/

            migrationBuilder.DropTable(
                name: "TournamentRequirements");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AgeWeightCategories");

            migrationBuilder.DropTable(
                name: "AgeCategories");

            migrationBuilder.DropTable(
                name: "WeightCategories");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Students");

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
                        name: "FK_TournamentRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TournamentRequests_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfEntry", "Gender" },
                values: new object[] { new DateTime(2015, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfEntry", "Gender" },
                values: new object[] { new DateTime(2014, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfEntry", "Gender" },
                values: new object[] { new DateTime(2018, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateOfEntry", "Gender" },
                values: new object[] { new DateTime(2013, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRequests_StudentId",
                table: "TournamentRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRequests_TournamentId",
                table: "TournamentRequests",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.DropTable(
                name: "TournamentRequests");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "AgeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndAge = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndWeight = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartWeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgeWeightCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeCategoryId = table.Column<int>(type: "int", nullable: true),
                    WeightCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeWeightCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgeWeightCategories_AgeCategories_AgeCategoryId",
                        column: x => x.AgeCategoryId,
                        principalTable: "AgeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AgeWeightCategories_WeightCategories_WeightCategoryId",
                        column: x => x.WeightCategoryId,
                        principalTable: "WeightCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeWeightCategoryId = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AgeWeightCategories_AgeWeightCategoryId",
                        column: x => x.AgeWeightCategoryId,
                        principalTable: "AgeWeightCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TournamentRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentRequirements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TournamentRequirements_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
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

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfEntry", "Gender", "Height", "Weight" },
                values: new object[] { new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 175, 88.0 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateOfEntry", "Gender", "Height", "Weight" },
                values: new object[] { new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 180, 87.0 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfEntry", "Gender", "Height", "Weight" },
                values: new object[] { new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 175, 81.0 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateOfEntry", "Gender", "Height", "Weight" },
                values: new object[] { new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 176, 73.0 });

            migrationBuilder.InsertData(
                table: "WeightCategories",
                columns: new[] { "Id", "EndWeight", "Name", "StartWeight" },
                values: new object[,]
                {
                    { 9, 75, "Middleweight", 70 },
                    { 8, 63, "Lightweight", 60 },
                    { 10, null, "Heavyweight", 80 },
                    { 6, 75, "Middleweight", 69 },
                    { 5, 60, "Lightweight", 57 },
                    { 7, null, "Heavyweight", 81 },
                    { 4, null, "Super heavyweight", 91 },
                    { 2, 75, "Middleweight", 69 },
                    { 3, 91, "Heavyweight", 81 },
                    { 1, 60, "Lightweight", 56 }
                });

            migrationBuilder.InsertData(
                table: "AgeWeightCategories",
                columns: new[] { "Id", "AgeCategoryId", "WeightCategoryId" },
                values: new object[,]
                {
                    { 3, 2, 3 },
                    { 10, 1, 10 },
                    { 16, 3, 6 },
                    { 6, 2, 6 },
                    { 15, 3, 5 },
                    { 5, 2, 5 },
                    { 17, 3, 7 },
                    { 8, 1, 8 },
                    { 7, 2, 7 },
                    { 4, 2, 4 },
                    { 12, 3, 2 },
                    { 2, 2, 2 },
                    { 11, 3, 1 },
                    { 1, 2, 1 },
                    { 13, 3, 3 },
                    { 14, 3, 4 },
                    { 9, 1, 9 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "AgeWeightCategoryId", "Gender" },
                values: new object[,]
                {
                    { 9, 3, 1 },
                    { 4, 8, 0 },
                    { 1, 8, 1 },
                    { 6, 10, 0 },
                    { 3, 10, 1 },
                    { 19, 16, 0 },
                    { 12, 6, 0 },
                    { 18, 15, 0 },
                    { 11, 5, 0 },
                    { 20, 17, 0 },
                    { 13, 7, 0 },
                    { 17, 14, 1 },
                    { 10, 4, 1 },
                    { 15, 12, 1 },
                    { 8, 2, 1 },
                    { 14, 11, 1 },
                    { 7, 1, 1 },
                    { 16, 13, 1 },
                    { 2, 9, 1 },
                    { 5, 9, 0 }
                });

            migrationBuilder.InsertData(
                table: "TournamentRequirements",
                columns: new[] { "Id", "CategoryId", "TournamentId" },
                values: new object[,]
                {
                    { 3, 9, 1 },
                    { 17, 2, 2 },
                    { 22, 4, 4 },
                    { 25, 1, 4 },
                    { 16, 1, 2 },
                    { 24, 6, 4 },
                    { 27, 3, 4 },
                    { 18, 3, 2 },
                    { 20, 19, 3 },
                    { 6, 12, 1 },
                    { 19, 18, 3 },
                    { 5, 11, 1 },
                    { 26, 2, 4 },
                    { 21, 20, 3 },
                    { 15, 17, 2 },
                    { 11, 10, 2 },
                    { 4, 10, 1 },
                    { 13, 15, 2 },
                    { 9, 8, 2 },
                    { 2, 8, 1 },
                    { 12, 14, 2 },
                    { 8, 7, 2 },
                    { 1, 7, 1 },
                    { 14, 16, 2 },
                    { 10, 9, 2 },
                    { 7, 13, 1 },
                    { 23, 5, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgeWeightCategories_AgeCategoryId",
                table: "AgeWeightCategories",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeWeightCategories_WeightCategoryId",
                table: "AgeWeightCategories",
                column: "WeightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AgeWeightCategoryId",
                table: "Categories",
                column: "AgeWeightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRequirements_CategoryId",
                table: "TournamentRequirements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRequirements_TournamentId",
                table: "TournamentRequirements",
                column: "TournamentId");
        }
    }
}
