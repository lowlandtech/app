using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class Add_Stack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Example = table.Column<string>(nullable: true),
                    CardId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsRoot = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Data = table.Column<int>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    ContentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stacks_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stacks_Stacks_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Stacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_CardId",
                table: "Contents",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Stacks_ContentId",
                table: "Stacks",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stacks_ParentId",
                table: "Stacks",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stacks");

            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
