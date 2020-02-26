using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class addingcardattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CardAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CardId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardAttributes_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardAttributes_CardId",
                table: "CardAttributes",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardAttributes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
