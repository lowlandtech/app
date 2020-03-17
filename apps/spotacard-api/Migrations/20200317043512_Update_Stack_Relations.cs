using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class Update_Stack_Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Cards_CardId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Stacks_Contents_ContentId",
                table: "Stacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContentId",
                table: "Stacks",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Stacks",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CardId",
                table: "Contents",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Contents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stacks_CardId",
                table: "Stacks",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Cards_CardId",
                table: "Contents",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stacks_Cards_CardId",
                table: "Stacks",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stacks_Contents_ContentId",
                table: "Stacks",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Cards_CardId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Stacks_Cards_CardId",
                table: "Stacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stacks_Contents_ContentId",
                table: "Stacks");

            migrationBuilder.DropIndex(
                name: "IX_Stacks_CardId",
                table: "Stacks");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Stacks");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Contents");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContentId",
                table: "Stacks",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CardId",
                table: "Contents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Cards_CardId",
                table: "Contents",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stacks_Contents_ContentId",
                table: "Stacks",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
