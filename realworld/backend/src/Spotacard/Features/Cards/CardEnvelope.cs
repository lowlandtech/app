using Spotacard.Domain;

namespace Spotacard.Features.Cards
{
    public class ArticleEnvelope
    {
        public ArticleEnvelope(Card card)
        {
            Card = card;
        }

        public Card Card { get; }
    }
}