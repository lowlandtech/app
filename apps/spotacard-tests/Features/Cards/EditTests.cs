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
        public async Task Expect_Edit_Card()
        {
            var createCommand = new Create.Command()
            {
                Card = new Create.CardData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = "tag1,tag2"
                }
            };

            var createdCard = await CardHelpers.CreateCard(this, createCommand);

            var command = new Edit.Command()
            {
                Card = new Edit.CardData()
                {
                    Title = "Updated " + createdCard.Title,
                    Description = "Updated" + createdCard.Description,
                    Body = "Updated" + createdCard.Body,
                },
                Slug = createdCard.Slug
            };
            // remove the first tag and add a new tag
            command.Card.TagList = new string[] { createdCard.TagList[1], "tag3" };

            var dbContext = GetDbContext();

            var cardEditHandler = new Edit.Handler(dbContext);
            var edited = await cardEditHandler.Handle(command, new System.Threading.CancellationToken());

            Assert.NotNull(edited);
            Assert.Equal(edited.Card.Title, command.Card.Title);
            Assert.Equal(edited.Card.TagList.Count(), command.Card.TagList.Count());
            // use assert Contains because we do not know the order in which the tags are saved/retrieved
            Assert.Contains(edited.Card.TagList[0], command.Card.TagList);
            Assert.Contains(edited.Card.TagList[1], command.Card.TagList);
        }
    }
}
