using System.Threading.Tasks;
using NUnit.Framework;

namespace Spotacard.Features.Graphs
{
    public class GraphTests
    {
        [Test]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            var fixture = new SliceFixture();
            try
            {
                var graph = fixture.GetGraph();
                Assert.True(await graph.Database.CanConnectAsync());
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
