using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class FixedMistakeInTournametsEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AgeCategories_AgeCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Tournaments_TournamentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_WeightCategories_WeightCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AgeCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TournamentId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_WeightCategoryId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DropColumn(
                name: "AgeCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "WeightCategoryId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "EndWeight",
                table: "WeightCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AgeWeightCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgeWeightCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightCategoryId = table.Column<int>(type: "int", nullable: true),
                    AgeCategoryId = table.Column<int>(type: "int", nullable: true)
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
                name: "TournamentRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    TournamentId = table.Column<int>(type: "int", nullable: true)
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
                table: "AgeWeightCategories",
                columns: new[] { "Id", "AgeCategoryId", "WeightCategoryId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 17, 3, 7 },
                    { 16, 3, 6 },
                    { 15, 3, 5 },
                    { 14, 3, 4 },
                    { 13, 3, 3 },
                    { 12, 3, 2 },
                    { 11, 3, 1 },
                    { 10, 1, 10 },
                    { 8, 1, 8 },
                    { 7, 2, 7 },
                    { 6, 2, 6 },
                    { 5, 2, 5 },
                    { 4, 2, 4 },
                    { 3, 2, 3 },
                    { 2, 2, 2 },
                    { 9, 1, 9 }
                });

            migrationBuilder.InsertData(
                table: "TournamentRequirements",
                columns: new[] { "Id", "CategoryId", "TournamentId" },
                values: new object[,]
                {
                    { 23, 5, 4 },
                    { 24, 6, 4 },
                    { 25, 1, 2 },
                    { 26, 2, 2 },
                    { 4, 10, 1 },
                    { 3, 9, 1 },
                    { 2, 8, 1 },
                    { 1, 7, 1 },
                    { 27, 3, 2 },
                    { 21, 20, 3 },
                    { 22, 4, 4 },
                    { 19, 18, 3 },
                    { 5, 11, 1 },
                    { 6, 12, 1 },
                    { 7, 13, 1 }
                });

            migrationBuilder.InsertData(
                table: "TournamentRequirements",
                columns: new[] { "Id", "CategoryId", "TournamentId" },
                values: new object[,]
                {
                    { 8, 7, 2 },
                    { 20, 19, 3 },
                    { 10, 9, 2 },
                    { 9, 8, 2 },
                    { 13, 15, 2 },
                    { 14, 16, 2 },
                    { 15, 17, 2 },
                    { 16, 1, 2 },
                    { 17, 2, 2 },
                    { 18, 3, 2 },
                    { 11, 10, 2 },
                    { 12, 14, 2 }
                });

            migrationBuilder.UpdateData(
                table: "WeightCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "EndWeight",
                value: null);

            migrationBuilder.UpdateData(
                table: "WeightCategories",
                keyColumn: "Id",
                keyValue: 7,
                column: "EndWeight",
                value: null);

            migrationBuilder.UpdateData(
                table: "WeightCategories",
                keyColumn: "Id",
                keyValue: 10,
                column: "EndWeight",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "AgeWeightCategoryId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "AgeWeightCategoryId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "AgeWeightCategoryId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 8, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 9, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 10, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                column: "AgeWeightCategoryId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 5, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 6, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                column: "AgeWeightCategoryId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 11, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 12, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                column: "AgeWeightCategoryId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                column: "AgeWeightCategoryId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 15, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AgeWeightCategoryId", "Gender" },
                values: new object[] { 16, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20,
                column: "AgeWeightCategoryId",
                value: 17);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AgeWeightCategoryId",
                table: "Categories",
                column: "AgeWeightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeWeightCategories_AgeCategoryId",
                table: "AgeWeightCategories",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeWeightCategories_WeightCategoryId",
                table: "AgeWeightCategories",
                column: "WeightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRequirements_CategoryId",
                table: "TournamentRequirements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRequirements_TournamentId",
                table: "TournamentRequirements",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AgeWeightCategories_AgeWeightCategoryId",
                table: "Categories",
                column: "AgeWeightCategoryId",
                principalTable: "AgeWeightCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AgeWeightCategories_AgeWeightCategoryId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "AgeWeightCategories");

            migrationBuilder.DropTable(
                name: "TournamentRequirements");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AgeWeightCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AgeWeightCategoryId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "EndWeight",
                table: "WeightCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, "Lightweight - Boys-juniors", 1, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, "Middleweight - Boys-juniors", 1, 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, "Heavyweight - Boys-juniors", 1, 3 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 1, 1, "Lightweight - Boys 15-16 years old", 2, 8 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 1, 1, "Middlweight - Boys 15-16 years old", 2, 9 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 1, 1, "Heavyweight - Boys 15-16 years old", 2, 10 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 1, 0, "Lightweight - Girls 15-16 years old", 2, 8 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 1, 0, "Middleweight - Girls 15-16 years old", 2, 9 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 1, 0, "Heavyweight - Girls 15-16 years old", 2, 10 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, "Lightweight - Boys-juniors", 2, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, 1, "Middleweight - Boys-juniors", 2, 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, 1, "Heavyweight - Boys-juniors", 2, 3 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, "Lightweight - Girls-juniors", 2, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, 0, "Middleweight - Girls-juniors", 2, 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 2, 0, "Heavyweight - Girls-juniors", 2, 3 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 3, "Lightweight - Males", 4, 1 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 3, "Miidleweight - Males", 4, 2 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 3, 1, "Heavyweight - Males", 4, 3 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 3, 1, "Super heavyweight - Males", 4, 4 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AgeCategoryId", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[] { 3, "Lightweight - Females", 3, 5 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "AgeCategoryId", "Gender", "Name", "TournamentId", "WeightCategoryId" },
                values: new object[,]
                {
                    { 22, 3, 0, "Heavyweight - Females", 3, 7 },
                    { 21, 3, 0, "Miidleweight - Females", 3, 6 }
                });

            migrationBuilder.UpdateData(
                table: "WeightCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "EndWeight",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WeightCategories",
                keyColumn: "Id",
                keyValue: 7,
                column: "EndWeight",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WeightCategories",
                keyColumn: "Id",
                keyValue: 10,
                column: "EndWeight",
                value: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AgeCategories_AgeCategoryId",
                table: "Categories",
                column: "AgeCategoryId",
                principalTable: "AgeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Tournaments_TournamentId",
                table: "Categories",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_WeightCategories_WeightCategoryId",
                table: "Categories",
                column: "WeightCategoryId",
                principalTable: "WeightCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
