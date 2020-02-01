using System.Linq;
using Spotacard.Domain;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Cards
{
    public static class CardExtensions
    {
        public static IQueryable<Card> GetAllData(this DbSet<Card> cards)
        {
            return cards
                .Include(x => x.Author)
                .Include(x => x.CardFavorites)
                .Include(x => x.CardTags)
                .AsNoTracking();
        }
    }
}