using System.Linq;
using Spotacard.Domain;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Cards
{
    public static class ArticleExtensions
    {
        public static IQueryable<Card> GetAllData(this DbSet<Card> articles)
        {
            return articles
                .Include(x => x.Author)
                .Include(x => x.ArticleFavorites)
                .Include(x => x.ArticleTags)
                .AsNoTracking();
        }
    }
}