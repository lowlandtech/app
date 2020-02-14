using System.Collections.Generic;

namespace Spotacard.Core.Types
{
  public abstract class Seeder : ISeeder
  {
    public virtual void Execute()
    {
      foreach (var activity in Activities) activity.Execute();
    }

    public List<IActivity> Activities { get; set; } = new List<IActivity>();
  }
}
