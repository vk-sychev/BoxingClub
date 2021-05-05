using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class AddFighterExperienceSpecs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experienced",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "FighterExperienceSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingPeriod = table.Column<int>(type: "int", nullable: false),
                    NumberOfFights = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterExperienceSpecifications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FighterExperienceSpecifications",
                columns: new[] { "Id", "NumberOfFights", "TrainingPeriod" },
                values: new object[,]
                {
                    { 1, 5, 3 },
                    { 2, 3, 2 },
                    { 3, 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FighterExperienceSpecifications");

            migrationBuilder.AddColumn<bool>(
                name: "Experienced",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
