using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class Added_ReferentialActionSetNull_to_FK_Students_BoxingGroups_BoxingGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Students_BoxingGroups_BoxingGroupId", "Students");
            migrationBuilder.AddForeignKey("FK_Students_BoxingGroups_BoxingGroupId",
                                           "Students",
                                           "BoxingGroupId",
                                           "BoxingGroups",
                                           principalColumn: "Id",
                                           onDelete: ReferentialAction.SetNull);         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Students_BoxingGroups_BoxingGroupId", "Students");
            migrationBuilder.AddForeignKey("FK_Students_BoxingGroups_BoxingGroupId",
                               "Students",
                               "BoxingGroupId",
                               "BoxingGroups",
                               principalColumn: "Id",
                               onDelete: ReferentialAction.SetNull);    
        }
    }
}
