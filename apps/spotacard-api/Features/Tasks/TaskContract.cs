using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.Tasks
{
    public class TaskContract : IContract
    {
        public TaskContract(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
