using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class AddUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardAttributes_Cards_CardId",
                table: "CardAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardAttributes",
                table: "CardAttributes");

            migrationBuilder.RenameTable(
                name: "CardAttributes",
                newName: "Attributes");

            migrationBuilder.RenameIndex(
                name: "IX_CardAttributes_CardId",
                table: "Attributes",
                newName: "IX_Attributes_CardId");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Cards",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Attributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Slug",
                table: "Cards",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Cards_CardId",
                table: "Attributes",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Cards_CardId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Cards_Slug",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Attributes");

            migrationBuilder.RenameTable(
                name: "Attributes",
                newName: "CardAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_Attributes_CardId",
                table: "CardAttributes",
                newName: "IX_CardAttributes_CardId");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardAttributes",
                table: "CardAttributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardAttributes_Cards_CardId",
                table: "CardAttributes",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
