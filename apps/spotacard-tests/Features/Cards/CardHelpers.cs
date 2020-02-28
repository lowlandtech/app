using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Users;

namespace Spotacard.Features.Cards
{
    public static class CardHelpers
    {
        /// <summary>
        ///     creates an card based on the given Create command. It also creates a default user
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<Card> CreateCard(TestFixture fixture, Create.Command command)
        {
            // first create the default user
            var user = await UserHelpers.CreateDefaultUser(fixture);

            var context = fixture.GetContext();
            var currentAccessor = new StubCurrentUserAccessor(user.Username);

            var handler = new Create.Handler(context, currentAccessor);
            var created = await handler.Handle(command, new CancellationToken());

            var card = await fixture
                .ExecuteDbContextAsync(_context => _context.Cards
                    .Where(_card => _card.Id == created.Card.Id)
                    .SingleOrDefaultAsync());

            return card;
        }
    }
}
