using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerWeb.Migrations
{
    public partial class FixSeverityColumnBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "Bug",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Severity",
                table: "Bug");
        }
    }
}
