using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class ChahgedConstraintFK_BoxingGroups_AspNetUsers_CoachId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_BoxingGroups_AspNetUsers_CoachId", "BoxingGroups");
            migrationBuilder.AddForeignKey(
                name: "FK_BoxingGroups_AspNetUsers_CoachId",
                table: "BoxingGroups",
                column: "CoachId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_BoxingGroups_AspNetUsers_CoachId", "BoxingGroups");
            migrationBuilder.AddForeignKey(
                name: "FK_BoxingGroups_AspNetUsers_CoachId",
                table: "BoxingGroups",
                column: "CoachId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
