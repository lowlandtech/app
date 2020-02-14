using System;

namespace Spotacard.Domain
{
    public class CardTag
    {
        public Guid CardId { get; set; }
        public Card Card { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
