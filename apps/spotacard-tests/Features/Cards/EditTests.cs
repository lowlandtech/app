using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Spotacard.Features.Cards
{
    public class EditTests
    {
        [Test]
        public async Task Expect_Edit_Card()
        {
            var fixture = new SliceFixture();
            var createCommand = new Create.Command
            {
                Card = new Create.CardData
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = "tag1,tag2"
                }
            };

            var created = await CardHelpers.CreateCard(fixture, createCommand);
            var editCommand = new Edit.Command
            {
                Card = new Edit.CardData
                {
                    Title = "Updated " + created.Title,
                    Description = "Updated" + created.Description,
                    Body = "Updated" + created.Body
                },
                Slug = created.Slug
            };

            // remove the first tag and add a new tag
            editCommand.Card.TagList = $"{created.TagList[1]}, tag3";

            var graph = fixture.GetGraph();
            var handler = new Edit.Handler(graph);
            var edited = await handler.Handle(editCommand, new CancellationToken());

            Assert.That(edited, Is.Not.Null);
            Assert.That(edited.Card.Title, Is.EqualTo(editCommand.Card.Title));
            Assert.That(edited.Card.TagList.Count(), Is.EqualTo(editCommand.Card.TagList.Split(",").Count()));
            // use assert Contains because we do not know the order in which the tags are saved/retrieved
            Assert.That(editCommand.Card.TagList.Contains(edited.Card.TagList[0]), Is.True);
            Assert.That(editCommand.Card.TagList.Contains(edited.Card.TagList[1]), Is.True);
        }
    }
}
