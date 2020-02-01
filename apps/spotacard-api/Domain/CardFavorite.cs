namespace Spotacard.Domain
{
    public class CardFavorite
    {
        public int CardId { get; set; }
        public Card Card { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}