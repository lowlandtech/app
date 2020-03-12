using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Users;
using Create = Spotacard.Features.Fields.Commands.Create;

namespace Spotacard.Features.Fields
{
    public class FieldHelpers
    {
        /// <summary>
        ///     creates an field based on the given Create command. It also creates a default user
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<Field> CreateField(TestFixture fixture, Create.Command command)
        {
            // first create the default user
            var user = await UserHelpers.CreateDefaultUser(fixture);
            var currentAccessor = new StubCurrentUser(user.Username);

            var handler = new Create.Handler(fixture.GetContext(), currentAccessor);
            var result = await handler.Handle(command, new CancellationToken());

            var created = await fixture
                .ExecuteDbContextAsync(context => context.Fields
                    .Where(field => field.Id == result.Field.Id)
                    .SingleOrDefaultAsync());

            return created;
        }
    }
}
