using Spotacard.Domain;

namespace Spotacard.Features.Attributes
{
    public class AttributeEnvelope
    {
        public AttributeEnvelope(CardAttribute attribute)
        {
            Attribute = attribute;
        }
        public CardAttribute Attribute { get; }
    }
}
