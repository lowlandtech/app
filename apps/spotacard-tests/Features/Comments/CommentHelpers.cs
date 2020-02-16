using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Users;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Comments
{
  public static class CommentHelpers
  {
    /// <summary>
    ///   creates an card comment based on the given Create command.
    ///   Creates a default user if parameter userName is empty.
    /// </summary>
    /// <param name="fixture"></param>
    /// <param name="command"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static async Task<Comment> CreateComment(SliceFixture fixture, Create.Command command, string userName)
    {
      if (string.IsNullOrWhiteSpace(userName))
      {
        var user = await UserHelpers.CreateDefaultUser(fixture);
        userName = user.Username;
      }

      var graph = fixture.GetGraph();
      var currentAccessor = new StubCurrentUserAccessor(userName);

      var handler = new Create.Handler(graph, currentAccessor);
      var created = await handler.Handle(command, new CancellationToken());

      var card = await fixture.ExecuteDbContextAsync(
        _graph => _graph.Cards
          .Include(_card => _card.Comments)
          .Include(_card => _card.Author)
          .Where(_card => _card.Slug == command.Slug)
          .SingleOrDefaultAsync()
      );

      var comments = card.Comments
        .FirstOrDefault(
          _comment =>
            _comment.CardId == card.Id &&
            _comment.Author == card.Author
        );

      return comments;
    }
  }
}
