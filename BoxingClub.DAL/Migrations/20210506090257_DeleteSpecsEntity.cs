using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class DeleteSpecsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FighterExperienceSpecifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FighterExperienceSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfFights = table.Column<int>(type: "int", nullable: false),
                    TrainingPeriod = table.Column<int>(type: "int", nullable: false)
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
    }
}
