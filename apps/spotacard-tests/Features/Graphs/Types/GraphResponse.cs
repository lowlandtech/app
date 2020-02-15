using Microsoft.Data.Sqlite;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Graphs.Types
{
  public class GraphResponse
  {
    public SqliteConnection Connection { get; set; }
    public GraphContext Graph { get; set; }
  }
}
