using Spotacard.Domain;

namespace Spotacard.Features.Relations.Types
{
    public class RelationEnvelope
    {
        public Relation Relation { get; }

        public RelationEnvelope(Relation relation)
        {
            Relation = relation;
        }
    }
}
