using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class Change_Packages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Package",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Layouts");

            migrationBuilder.AddColumn<string>(
                name: "Packages",
                table: "Widgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Packages",
                table: "Layouts",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wiring",
                table: "Layouts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Fields",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Packages",
                table: "Widgets");

            migrationBuilder.DropColumn(
                name: "Packages",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "Wiring",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "Package",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Layouts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
