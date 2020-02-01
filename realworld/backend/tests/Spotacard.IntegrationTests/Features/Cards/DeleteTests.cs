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
        public async Task Expect_Delete_Article()
        {
            var createCmd = new Create.Command()
            {
                Card = new Create.ArticleData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                }
            };

            var card = await ArticleHelpers.CreateArticle(this, createCmd);
            var slug = card.Slug;

            var deleteCmd = new Delete.Command(slug);

            var dbContext = GetDbContext();

            var articleDeleteHandler = new Delete.QueryHandler(dbContext);
            await articleDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var dbArticle = await ExecuteDbContextAsync(db => db.Cards.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());

            Assert.Null(dbArticle);
        }

        [Fact]
        public async Task Expect_Delete_Article_With_Tags()
        {
            var createCmd = new Create.Command()
            {
                Card = new Create.ArticleData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = new string[] { "tag1", "tag2" }
                }
            };

            var card = await ArticleHelpers.CreateArticle(this, createCmd);
            var dbArticleWithTags = await ExecuteDbContextAsync(
                db => db.Cards.Include(a => a.ArticleTags)
                .Where(d => d.Slug == card.Slug).SingleOrDefaultAsync()
            );

            var deleteCmd = new Delete.Command(card.Slug);

            var dbContext = GetDbContext();

            var articleDeleteHandler = new Delete.QueryHandler(dbContext);
            await articleDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var dbArticle = await ExecuteDbContextAsync(db => db.Cards.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());
            Assert.Null(dbArticle);
        }

        [Fact]
        public async Task Expect_Delete_Article_With_Comments()
        {
            var createArticleCmd = new Create.Command()
            {
                Card = new Create.ArticleData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                }
            };

            var card = await ArticleHelpers.CreateArticle(this, createArticleCmd);
            var dbArticle = await ExecuteDbContextAsync(
                db => db.Cards.Include(a => a.ArticleTags)
                .Where(d => d.Slug == card.Slug).SingleOrDefaultAsync()
            );

            var articleId = dbArticle.ArticleId;
            var slug = dbArticle.Slug;

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

            var articleDeleteHandler = new Delete.QueryHandler(dbContext);
            await articleDeleteHandler.Handle(deleteCmd, new System.Threading.CancellationToken());

            var deleted = await ExecuteDbContextAsync(db => db.Cards.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());
            Assert.Null(deleted);
        }
    }
}
