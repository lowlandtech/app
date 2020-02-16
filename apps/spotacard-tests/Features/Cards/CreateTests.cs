using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Spotacard.Features.Cards
{
  public class CreateTests : SliceFixture
  {
    [Test]
    public async Task Expect_Create_Card()
    {
      var command = new Create.Command
      {
        Card = new Create.CardData
        {
          Title = "Test card dsergiu77",
          Description = "Description of the test card",
          Body = "Body of the test card",
          TagList = "tag1,tag2"
        }
      };

      var card = await CardHelpers.CreateCard(this, command);

      Assert.That(card, Is.Not.Null);
      Assert.That(card.Title, Is.EqualTo(command.Card.Title));
      Assert.That(card.TagList.Count(), Is.EqualTo(command.Card.TagList.Count()));
    }
  }
}
