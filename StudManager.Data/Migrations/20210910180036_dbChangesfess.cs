using Microsoft.EntityFrameworkCore.Migrations;

namespace StudManager.Data.Migrations
{
    public partial class dbChangesfess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeesDescription",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeesType",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StuId",
                table: "Fees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeesDescription",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "FeesType",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "StuId",
                table: "Fees");
        }
    }
}
