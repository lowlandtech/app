using NUnit.Framework;
using System.Threading.Tasks;

namespace Spotacard.Features.Graphs
{
  public class GraphTests
  {
    [Test]
    public async Task DatabaseIsAvailableAndCanBeConnectedTo()
    {
      var response = GraphFixtures.Get();
      try
      {
        Assert.True(await response.Graph.Database.CanConnectAsync());
      }
      finally
      {
        response.Connection.Close();
      }
    }
  }
}
