using System.Collections.Generic;
using Spotacard.Domain;

namespace Spotacard.Features.Attributes
{
    public class AttributesEnvelope
    {
        public AttributesEnvelope(List<CardAttribute> attributes)
        {
            Attributes = attributes;
        }

        public List<CardAttribute> Attributes { get; }
    }
}
