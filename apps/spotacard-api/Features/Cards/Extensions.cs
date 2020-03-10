using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Infrastructure;

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

        /// <summary>
        ///     get the list of Tags to be added
        /// </summary>
        /// <param name="cardTagList"></param>
        /// <returns></returns>
        internal static async Task<List<Tag>> GetTagsToCreate(this IEnumerable<string> cardTagList, GraphContext _context)
        {
            var tagsToCreate = new List<Tag>();
            foreach (var tag in cardTagList)
            {
                var t = await _context.Tags.FindAsync(tag);
                if (t == null)
                {
                    t = new Tag
                    {
                        TagId = tag
                    };
                    tagsToCreate.Add(t);
                }
            }

            return tagsToCreate;
        }

        /// <summary>
        ///     check which card tags need to be added
        /// </summary>
        internal static List<CardTag> GetCardTagsToCreate(this Card card, IEnumerable<string> cardTagList, GraphContext context)
        {
            var cardTagsToCreate = new List<CardTag>();
            foreach (var tag in cardTagList)
            {
                var tagData = context.Tags.Find(tag) ?? new Tag { TagId = tag };

                var at = card.CardTags.FirstOrDefault(t => t.TagId == tag);

                if (at == null)
                {
                    at = new CardTag
                    {
                        Card = card,
                        CardId = card.Id,
                        Tag = tagData,
                        TagId = tag
                    };
                    cardTagsToCreate.Add(at);
                }
            }

            return cardTagsToCreate;
        }

        /// <summary>
        ///     check which card tags need to be deleted
        /// </summary>
        internal static List<CardTag> GetCardTagsToDelete(this Card card, IEnumerable<string> cardTagList)
        {
            var cardTagsToDelete = new List<CardTag>();
            foreach (var tag in card.CardTags)
            {
                var at = cardTagList.FirstOrDefault(t => t == tag.TagId);
                if (at == null) cardTagsToDelete.Add(tag);
            }

            return cardTagsToDelete;
        }
    }
}
