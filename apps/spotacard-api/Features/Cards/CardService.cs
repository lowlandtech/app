using System.Collections.Generic;
using System.Linq;
using Spotacard.Domain;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Cards
{
    public class CardService
    {
        private readonly GraphContext _context;

        public CardService(GraphContext context)
        {
            _context = context;
        }

        public void Add(string name)
        {
            var node = new Card {Title = name};
            _context.Cards.Add(node);
            _context.SaveChanges();
        }

        public IEnumerable<Card> Find(string term)
        {
            return _context.Cards
                .Where(b => b.Title.Contains(term))
                .OrderBy(b => b.Title)
                .ToList();
        }
    }
}
