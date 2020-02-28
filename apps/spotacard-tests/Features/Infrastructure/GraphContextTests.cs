using System.Threading.Tasks;
using NUnit.Framework;

namespace Spotacard.Features.Infrastructure
{
    public class GraphContextTests
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
