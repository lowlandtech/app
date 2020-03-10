using Spotacard.Domain;

namespace Spotacard.Features.Templates.Types
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
