using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spotacard.Features.Cards
{
  public class CreateTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Create_Card()
        {
            var command = new Create.Command()
            {
                Card = new Create.CardData()
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = "tag1,tag2"
                }
            };

            var card = await CardHelpers.CreateCard(this, command);

            Assert.NotNull(card);
            Assert.Equal(card.Title, command.Card.Title);
            Assert.Equal(card.TagList.Count(), command.Card.TagList.Split(",").Count());
        }
    }
}
