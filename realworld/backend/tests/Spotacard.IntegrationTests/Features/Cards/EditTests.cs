using System.Linq;
using System.Threading.Tasks;
using Spotacard.Features.Cards;
using Spotacard.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Spotacard.IntegrationTests.Features.Cards
{
    public class EditTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Edit_Article()
        {
            var createCommand = new Create.Command()
            {
                Card = new Create.ArticleData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = new string[] { "tag1", "tag2" }
                }
            };

            var createdArticle = await ArticleHelpers.CreateArticle(this, createCommand);

            var command = new Edit.Command()
            {
                Card = new Edit.ArticleData()
                {
                    Title = "Updated " + createdArticle.Title,
                    Description = "Updated" + createdArticle.Description,
                    Body = "Updated" + createdArticle.Body,
                },
                Slug = createdArticle.Slug
            };
            // remove the first tag and add a new tag
            command.Card.TagList = new string[] { createdArticle.TagList[1], "tag3" };

            var dbContext = GetDbContext();

            var articleEditHandler = new Edit.Handler(dbContext);
            var edited = await articleEditHandler.Handle(command, new System.Threading.CancellationToken());

            Assert.NotNull(edited);
            Assert.Equal(edited.Card.Title, command.Card.Title);
            Assert.Equal(edited.Card.TagList.Count(), command.Card.TagList.Count());
            // use assert Contains because we do not know the order in which the tags are saved/retrieved
            Assert.Contains(edited.Card.TagList[0], command.Card.TagList);
            Assert.Contains(edited.Card.TagList[1], command.Card.TagList);
        }
    }
}
