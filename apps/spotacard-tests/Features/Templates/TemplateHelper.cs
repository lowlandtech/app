using Spotacard.Features.Generator.Actions;
using Spotacard.Features.Generator.Types;
using Spotacard.Features.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Templates
{
    public static class TemplateHelper
    {
        /// <summary>
        ///     creates an card based on the given generate command. It also creates a default user
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<GenerateEnvelope> Generate(TestFixture fixture, Generate.Command command)
        {
            // first create the default user
            var user = await UserHelpers.CreateDefaultUser(fixture);
            var currentAccessor = new StubCurrentUser(user.Username);

            var handler = new Generate.Handler(fixture.GetContext(), currentAccessor);
            var result = await handler.Handle(command, new CancellationToken());

            return result;
        }
    }
}
