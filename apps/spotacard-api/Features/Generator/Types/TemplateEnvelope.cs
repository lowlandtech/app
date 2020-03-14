using Spotacard.Domain;

namespace Spotacard.Features.Generator.Types
{
    public class TemplateEnvelope
    {
        public Card Card { get; }

        public TemplateEnvelope(Card card)
        {
            Card = card;
        }
    }
}
