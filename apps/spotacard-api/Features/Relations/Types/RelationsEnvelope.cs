using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Relations.Types
{
    public class RelationsEnvelope
    {
        public List<Relation> Relations { get; set; }
        public int RelationsCount { get; set; }
    }
}
