using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Cards;
using Spotacard.IntegrationTests.Features.Comments;
using Spotacard.IntegrationTests.Features.Users;

namespace Spotacard.IntegrationTests.Features.Cards
{
    public class DeleteTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Delete_Card()
        {
            var createCmd = new Create.Command()
            {
                Card = new Create.CardData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                }
            };

            var card = await CardHelpers.CreateCard(this, createCmd);
            var slug = card.Slug;

            var deleteCmd = new Delete.Command(slug);

            var dbContext = GetDbContext();

            var cardDeleteHandler = new Delete.QueryHandler(dbContext);
            await cardDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var dbCard = await ExecuteDbContextAsync(db => db.Cards.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());

            Assert.Null(dbCard);
        }

        [Fact]
        public async Task Expect_Delete_Card_With_Tags()
        {
            var createCmd = new Create.Command()
            {
                Card = new Create.CardData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = "tag1,tag2"
                }
            };

            var card = await CardHelpers.CreateCard(this, createCmd);
            var dbCardWithTags = await ExecuteDbContextAsync(
                db => db.Cards.Include(a => a.CardTags)
                .Where(d => d.Slug == card.Slug).SingleOrDefaultAsync()
            );

            var deleteCmd = new Delete.Command(card.Slug);

            var dbContext = GetDbContext();

            var cardDeleteHandler = new Delete.QueryHandler(dbContext);
            await cardDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var dbCard = await ExecuteDbContextAsync(db => db.Cards.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());
            Assert.Null(dbCard);
        }

        [Fact]
        public async Task Expect_Delete_Card_With_Comments()
        {
            var createCardCmd = new Create.Command()
            {
                Card = new Create.CardData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                }
            };

            var card = await CardHelpers.CreateCard(this, createCardCmd);
            var dbCard = await ExecuteDbContextAsync(
                db => db.Cards.Include(a => a.CardTags)
                .Where(d => d.Slug == card.Slug).SingleOrDefaultAsync()
            );

            var cardId = dbCard.Id;
            var slug = dbCard.Slug;

            // create card comment
            var createCommentCmd = new Spotacard.Features.Comments.Create.Command()
            {
                Comment = new Spotacard.Features.Comments.Create.CommentData()
                {
                    Body = "card comment"
                },
                Slug = slug
            };

            var comment = await CommentHelpers.CreateComment(this, createCommentCmd, UserHelpers.DefaultUserName);

            // delete card with comment
            var deleteCmd = new Delete.Command(slug);

            var dbContext = GetDbContext();

            var cardDeleteHandler = new Delete.QueryHandler(dbContext);
            await cardDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var deleted = await ExecuteDbContextAsync(db => db.Cards.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());
            Assert.Null(deleted);
        }
    }
}
