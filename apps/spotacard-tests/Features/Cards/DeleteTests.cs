using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Features.Comments;
using Spotacard.Features.Users;

namespace Spotacard.Features.Cards
{
  public class DeleteTests : SliceFixture
  {
    [Test]
    public async Task Expect_Delete_Card()
    {
      var createCmd = new Create.Command
      {
        Card = new Create.CardData
        {
          Title = "Test card dsergiu77",
          Description = "Description of the test card",
          Body = "Body of the test card"
        }
      };

      var card = await CardHelpers.CreateCard(this, createCmd);
      var slug = card.Slug;

      var deleteCmd = new Delete.Command(slug);

      var graph = GetGraph();

      var cardDeleteHandler = new Delete.QueryHandler(graph);
      await cardDeleteHandler.Handle(deleteCmd, new CancellationToken());

      var dbCard = await ExecuteDbContextAsync(_graph => _graph.Cards.Where(d => d.Slug == deleteCmd.Slug).SingleOrDefaultAsync());

      Assert.Null(dbCard);
    }

    [Test]
    public async Task Expect_Delete_Card_With_Tags()
    {
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

      var card = await CardHelpers.CreateCard(this, createCmd);
      var cardWithTags = await ExecuteDbContextAsync(_graph => _graph.Cards
        .Include(a => a.CardTags)
          .Where(d => d.Slug == card.Slug).SingleOrDefaultAsync()
      );

      var deleteCmd = new Delete.Command(card.Slug);

      var graph = GetGraph();

      var handler = new Delete.QueryHandler(graph);
      await handler.Handle(deleteCmd, new CancellationToken());

      var dbCard = await ExecuteDbContextAsync(_graph => _graph.Cards
        .Where(d => d.Slug == deleteCmd.Slug)
        .SingleOrDefaultAsync());

      Assert.That(dbCard, Is.Null);
    }

    [Test]
    public async Task Expect_Delete_Card_With_Comments()
    {
      var createCardCmd = new Create.Command
      {
        Card = new Create.CardData
        {
          Title = "Test card dsergiu77",
          Description = "Description of the test card",
          Body = "Body of the test card"
        }
      };

      var card = await CardHelpers.CreateCard(this, createCardCmd);
      var dbCard = await ExecuteDbContextAsync(_graph =>
        _graph.Cards.Include(a => a.CardTags)
          .Where(d => d.Slug == card.Slug).SingleOrDefaultAsync()
      );

      var cardId = dbCard.Id;
      var slug = dbCard.Slug;

      // create card comment
      var createCommentCmd = new Comments.Create.Command
      {
        Comment = new Comments.Create.CommentData
        {
          Body = "card comment"
        },
        Slug = slug
      };

      var comment = await CommentHelpers.CreateComment(this, createCommentCmd, UserHelpers.DefaultUserName);

      // delete card with comment
      var deleteCmd = new Delete.Command(slug);

      var graph = GetGraph();

      var cardDeleteHandler = new Delete.QueryHandler(graph);
      await cardDeleteHandler.Handle(deleteCmd, new CancellationToken());

      var deleted =
        await ExecuteDbContextAsync(_graph => _graph.Cards.Where(_card => _card.Slug == deleteCmd.Slug).SingleOrDefaultAsync());
      Assert.That(deleted, Is.Null);
    }
  }
}
