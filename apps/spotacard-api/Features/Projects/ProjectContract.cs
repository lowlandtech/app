using Spotacard.Core.Contracts;
using Spotacard.Domain;

namespace Spotacard.Features.Projects
{
    public class ProjectContract : IContract
    {
        public ProjectContract(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
