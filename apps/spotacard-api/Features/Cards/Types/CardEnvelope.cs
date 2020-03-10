using Spotacard.Domain;

namespace Spotacard.Features.Cards.Types
{
    public class CardEnvelope
    {
        public CardEnvelope(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}
