using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotacard.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Persons",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Hash = table.Column<byte[]>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Persons", x => x.Id); });

            migrationBuilder.CreateTable(
                "Tags",
                table => new
                {
                    TagId = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_Tags", x => x.TagId); });

            migrationBuilder.CreateTable(
                "Cards",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Slug = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(),
                    UpdatedAt = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        "FK_Cards_Persons_AuthorId",
                        x => x.AuthorId,
                        "Persons",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "FollowedPeople",
                table => new
                {
                    ObserverId = table.Column<Guid>(),
                    TargetId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedPeople", x => new {x.ObserverId, x.TargetId});
                    table.ForeignKey(
                        "FK_FollowedPeople_Persons_ObserverId",
                        x => x.ObserverId,
                        "Persons",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_FollowedPeople_Persons_TargetId",
                        x => x.TargetId,
                        "Persons",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "CardFavorites",
                table => new
                {
                    CardId = table.Column<Guid>(),
                    PersonId = table.Column<Guid>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardFavorites", x => new {x.CardId, x.PersonId});
                    table.ForeignKey(
                        "FK_CardFavorites_Cards_CardId",
                        x => x.CardId,
                        "Cards",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CardFavorites_Persons_PersonId",
                        x => x.PersonId,
                        "Persons",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "CardTags",
                table => new
                {
                    CardId = table.Column<Guid>(),
                    TagId = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTags", x => new {x.CardId, x.TagId});
                    table.ForeignKey(
                        "FK_CardTags_Cards_CardId",
                        x => x.CardId,
                        "Cards",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CardTags_Tags_TagId",
                        x => x.TagId,
                        "Tags",
                        "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Body = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(),
                    CardId = table.Column<Guid>(),
                    CreatedAt = table.Column<DateTime>(),
                    UpdatedAt = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        "FK_Comments_Persons_AuthorId",
                        x => x.AuthorId,
                        "Persons",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Comments_Cards_CardId",
                        x => x.CardId,
                        "Cards",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_CardFavorites_PersonId",
                "CardFavorites",
                "PersonId");

            migrationBuilder.CreateIndex(
                "IX_Cards_AuthorId",
                "Cards",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_CardTags_TagId",
                "CardTags",
                "TagId");

            migrationBuilder.CreateIndex(
                "IX_Comments_AuthorId",
                "Comments",
                "AuthorId");

            migrationBuilder.CreateIndex(
                "IX_Comments_CardId",
                "Comments",
                "CardId");

            migrationBuilder.CreateIndex(
                "IX_FollowedPeople_TargetId",
                "FollowedPeople",
                "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CardFavorites");

            migrationBuilder.DropTable(
                "CardTags");

            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.DropTable(
                "FollowedPeople");

            migrationBuilder.DropTable(
                "Tags");

            migrationBuilder.DropTable(
                "Cards");

            migrationBuilder.DropTable(
                "Persons");
        }
    }
}
