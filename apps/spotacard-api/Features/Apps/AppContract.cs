using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.Apps
{
    public class AppContract : IContract
    {
        public AppContract(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
