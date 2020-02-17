using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.TaskLists
{
  public class TaskListContract : IContract
  {
    public TaskListContract(Card card)
    {
      Card = card;
    }
    public Card Card { get; }
  }
}
