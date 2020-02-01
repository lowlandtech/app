using System.Collections.Generic;
using Spotacard.Domain;

namespace Spotacard.Features.Cards
{
    public class ArticlesEnvelope
    {
        public List<Card> Cards { get; set; }

        public int ArticlesCount { get; set; }
    }
}