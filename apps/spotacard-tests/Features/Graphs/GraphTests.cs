using System.Threading.Tasks;
using NUnit.Framework;

namespace Spotacard.Features.Graphs
{
    public class GraphTests
    {
        [Test]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.True(await context.Database.CanConnectAsync());
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
