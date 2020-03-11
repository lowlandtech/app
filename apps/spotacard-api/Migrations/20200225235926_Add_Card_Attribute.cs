using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class AddCardAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Title",
                "Cards",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "Type",
                "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                "CardAttributes",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CardId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardAttributes", x => x.Id);
                    table.ForeignKey(
                        "FK_CardAttributes_Cards_CardId",
                        x => x.CardId,
                        "Cards",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_CardAttributes_CardId",
                "CardAttributes",
                "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CardAttributes");

            migrationBuilder.DropColumn(
                "Type",
                "Cards");

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Cards",
                "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
