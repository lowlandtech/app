using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cards
{
  public class DeleteTests
  {
    [Test]
    public async Task Expect_Delete_Card()
    {
      // arrange
      var fixture = new SliceFixture();
      var createCmd = new Create.Command
      {
        Card = new Create.CardData
        {
          Title = "Test card dsergiu77",
          Description = "Description of the test card",
          Body = "Body of the test card"
        }
      };

      var created = await CardHelpers.CreateCard(fixture, createCmd);
      var deleteCmd = new Delete.Command(created.Slug);
      var graph = fixture.GetGraph();
      var handler = new Delete.QueryHandler(graph);
      await handler.Handle(deleteCmd, new CancellationToken());

      // act
      var deleted = await fixture.ExecuteDbContextAsync(_graph => _graph.Cards
        .Where(_card => _card.Slug == deleteCmd.Slug)
        .SingleOrDefaultAsync());

      // assert
      Assert.That(deleted, Is.Null);
    }

    [Test]
    public async Task Expect_Delete_Card_With_Tags()
    {
      // arrange
      var fixture = new SliceFixture();
      var createCmd = new Create.Command
      {
        Card = new Create.CardData
        {
          Title = "Test card dsergiu77",
          Description = "Description of the test card",
          Body = "Body of the test card",
          TagList = "tag1,tag2"
        }
      };

      var created = await CardHelpers.CreateCard(fixture, createCmd);
      var deleteCmd = new Delete.Command(created.Slug);
      var graph = fixture.GetGraph();
      var handler = new Delete.QueryHandler(graph);
      await handler.Handle(deleteCmd, new CancellationToken());

      // act
      var deleted = await fixture.ExecuteDbContextAsync(_graph => _graph.Cards
        .Where(d => d.Slug == deleteCmd.Slug)
        .SingleOrDefaultAsync());

      // assert
      Assert.That(deleted, Is.Null);
    }

    [Test]
    public async Task Expect_Delete_Card_With_Comments()
    {
      // arrange
      var fixture = new SliceFixture();
      var createCardCmd = new Create.Command
      {
        Card = new Create.CardData
        {
          Title = "Test card dsergiu77",
          Description = "Description of the test card",
          Body = "Body of the test card"
        }
      };

      var created = await CardHelpers.CreateCard(fixture, createCardCmd);
      var deleteCmd = new Delete.Command(created.Slug);
      var graph = fixture.GetGraph();
      var handler = new Delete.QueryHandler(graph);
      await handler.Handle(deleteCmd, new CancellationToken());

      // act
      var deleted = await fixture.ExecuteDbContextAsync(_graph => _graph.Cards
        .Where(_card => _card.Slug == deleteCmd.Slug)
        .SingleOrDefaultAsync());

      // assert
      Assert.That(deleted, Is.Null);
    }
  }
}
