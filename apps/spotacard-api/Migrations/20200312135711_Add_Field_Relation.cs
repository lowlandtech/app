using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class Add_Field_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Apps",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    PkFieldId = table.Column<Guid>(nullable: false),
                    PkName = table.Column<string>(nullable: true),
                    FkFieldId = table.Column<Guid>(nullable: false),
                    FkName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relations_Fields_FkFieldId",
                        column: x => x.FkFieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relations_Fields_PkFieldId",
                        column: x => x.PkFieldId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relations_FkFieldId",
                table: "Relations",
                column: "FkFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_PkFieldId",
                table: "Relations",
                column: "PkFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Apps",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
