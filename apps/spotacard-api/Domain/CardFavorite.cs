using System;

namespace Spotacard.Domain
{
    public class CardFavorite
    {
        public Guid CardId { get; set; }
        public Card Card { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
