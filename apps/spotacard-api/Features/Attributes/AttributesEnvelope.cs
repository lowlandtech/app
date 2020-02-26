using Spotacard.Domain;
using System.Collections.Generic;

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
