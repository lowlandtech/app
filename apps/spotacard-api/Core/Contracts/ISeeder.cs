using System.Collections.Generic;

namespace Spotacard.Core
{
  public interface ISeeder : IActivity
  {
    List<IActivity> Activities { get; set; }
  }
}
