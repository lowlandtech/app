using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Graphs.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Graphs
{
  public static class GraphFixtures
  {
    private const string InMemoryConnectionString = "DataSource=:memory:";

    public static GraphResponse Get()
    {
      var connection = new SqliteConnection(InMemoryConnectionString);
      connection.Open();
      var options = new DbContextOptionsBuilder<GraphContext>()
        .UseSqlite(connection)
        .Options;
      var graph = new GraphContext(options);
      graph.Database.EnsureCreated();

      return new GraphResponse
      {
        Connection = connection,
        Graph = graph
      };
    }
  }
}
