using System.Collections.Generic;
using Spotacard.Domain;

namespace Spotacard.Features.Cards
{
    public class CardsEnvelope
    {
        public List<Card> Cards { get; set; }

        public int CardsCount { get; set; }
    }
}
