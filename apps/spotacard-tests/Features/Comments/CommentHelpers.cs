using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Users;

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

      var dbContext = fixture.GetDbContext();
      var currentAccessor = new StubCurrentUserAccessor(userName);

      var commentCreateHandler = new Create.Handler(dbContext, currentAccessor);
      var created = await commentCreateHandler.Handle(command, new CancellationToken());

      var dbCardWithComments = await fixture.ExecuteDbContextAsync(
        db => db.Cards
          .Include(a => a.Comments).Include(a => a.Author)
          .Where(a => a.Slug == command.Slug)
          .SingleOrDefaultAsync()
      );

      var dbComment = dbCardWithComments.Comments
        .Where(c => c.CardId == dbCardWithComments.Id && c.Author == dbCardWithComments.Author)
        .FirstOrDefault();

      return dbComment;
    }
  }
}
