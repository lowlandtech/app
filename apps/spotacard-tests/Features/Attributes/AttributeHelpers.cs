using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;

namespace Spotacard.Features.Attributes
{
    public class AttributeHelpers
    {
        public static async Task<CardAttribute> CreateCardAttribute(TestFixture fixture, Create.Command command)
        {
            var graph = fixture.GetGraph();

            var handler = new Create.Handler(graph);
            var created = await handler.Handle(command, new CancellationToken());

            var attribute = await fixture
                .ExecuteDbContextAsync(_graph => _graph.Attributes
                    .Where(_attribute => _attribute.Id == created.Attribute.Id)
                    .SingleOrDefaultAsync());

            return attribute;
        }

        public static async Task<AttributesEnvelope> ListCardAttribute(TestFixture fixture, List.Query command)
        {
            var graph = fixture.GetGraph();
            var handler = new List.QueryHandler(graph);
            return await handler.Handle(command, new CancellationToken());
        }
    }
}
