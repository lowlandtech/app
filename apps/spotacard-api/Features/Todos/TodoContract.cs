using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.Todos
{
    public class TodoContract : IContract
    {
        public TodoContract(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
