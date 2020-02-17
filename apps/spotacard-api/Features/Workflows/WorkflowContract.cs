using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.Workflows
{
  public class WorkflowContract : IContract
  {
    public WorkflowContract(Card card)
    {
      Card = card;
    }
    public Card Card { get; }
  }
}
