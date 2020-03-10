using System.Linq;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;

namespace Spotacard.Features.Cards
{
    internal static class Extensions
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
