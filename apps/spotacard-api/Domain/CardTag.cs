namespace Spotacard.Domain
{
    public class CardTag
    {
        public int CardId { get; set; }
        public Card Card { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}