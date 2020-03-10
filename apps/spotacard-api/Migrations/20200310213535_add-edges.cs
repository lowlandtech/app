using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class addedges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Edges",
                columns: table => new
                {
                    ParentId = table.Column<Guid>(nullable: false),
                    ChildId = table.Column<Guid>(nullable: false),
                    Label = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edges", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_Edges_Cards_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Edges_Cards_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edges_ChildId",
                table: "Edges",
                column: "ChildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Edges");
        }
    }
}
