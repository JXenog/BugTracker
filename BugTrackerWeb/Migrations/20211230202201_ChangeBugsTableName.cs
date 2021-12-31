using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerWeb.Migrations
{
    public partial class ChangeBugsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bug_Projects_ProjectId",
                table: "Bug");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bug",
                table: "Bug");

            migrationBuilder.RenameTable(
                name: "Bug",
                newName: "Bugs");

            migrationBuilder.RenameIndex(
                name: "IX_Bug_ProjectId",
                table: "Bugs",
                newName: "IX_Bugs_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs");

            migrationBuilder.RenameTable(
                name: "Bugs",
                newName: "Bug");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bug",
                newName: "IX_Bug_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bug",
                table: "Bug",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_Projects_ProjectId",
                table: "Bug",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
