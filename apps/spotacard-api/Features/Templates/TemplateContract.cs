using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.Templates
{
  public class TemplateContract : IContract
  {
    public TemplateContract(Card card)
    {
      Card = card;
    }
    public Card Card { get; }
  }
}
