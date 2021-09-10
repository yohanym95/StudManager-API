using Microsoft.EntityFrameworkCore.Migrations;

namespace StudManager.Data.Migrations
{
    public partial class dbchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Fees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Fees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
