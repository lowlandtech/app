using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.Steps
{
  public class StepContract : IContract
  {
    public StepContract(Card card)
    {
      Card = card;
    }
    public Card Card { get; }
  }
}
