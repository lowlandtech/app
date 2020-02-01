using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.IntegrationTests.Features.Users;
using Spotacard.Features.Cards;

namespace Spotacard.IntegrationTests.Features.Cards
{
    public static class ArticleHelpers
    {
        /// <summary>
        /// creates an card based on the given Create command. It also creates a default user
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<Domain.Card> CreateArticle(SliceFixture fixture, Create.Command command)
        {
            // first create the default user
            var user = await UserHelpers.CreateDefaultUser(fixture);

            var dbContext = fixture.GetDbContext();
            var currentAccessor = new StubCurrentUserAccessor(user.Username);

            var articleCreateHandler = new Create.Handler(dbContext, currentAccessor);
            var created = await articleCreateHandler.Handle(command, new System.Threading.CancellationToken());

            var dbArticle = await fixture.ExecuteDbContextAsync(db => db.Cards.Where(a => a.Id == created.Card.Id)
                .SingleOrDefaultAsync());

            return dbArticle;
        }
    }
}
