using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Users;
using Create = Spotacard.Features.Widgets.Commands.Create;

namespace Spotacard.Features.Widgets
{
    public class WidgetHelpers
    {
        /// <summary>
        ///     creates an widget based on the given Create command. It also creates a default user
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<Widget> CreateWidget(TestFixture fixture, Create.Command command)
        {
            // first create the default user
            var user = await UserHelpers.CreateDefaultUser(fixture);
            var currentAccessor = new StubCurrentUser(user.Username);

            var handler = new Create.Handler(fixture.GetContext(), currentAccessor);
            var result = await handler.Handle(command, new CancellationToken());

            var created = await fixture
                .ExecuteDbContextAsync(context => context.Widgets
                    .Where(widget => widget.Id == result.Widget.Id)
                    .SingleOrDefaultAsync());

            return created;
        }
    }
}
