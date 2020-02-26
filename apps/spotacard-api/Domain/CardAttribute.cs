using System;

namespace Spotacard.Domain
{
    public class CardAttribute
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }
        public Card Card { get; set; }
        public Guid CardId { get; set; }
    }
}
