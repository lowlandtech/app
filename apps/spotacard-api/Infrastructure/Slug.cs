using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Infrastructure
{
    //https://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
    //https://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net
    public static class Slug
    {
        public static async Task<string> ToSlugAsync(this string text, GraphContext context, CancellationToken cancellationToken)
        {
            var counter = 0;
            var slug = text.Pascalize().Kebaberize();
            Start:

            var card = await context.Cards.SingleOrDefaultAsync(c => c.Slug == slug, cancellationToken);
            if (card == null) return slug;

            counter += counter;
            slug = $"{text.Pascalize().Kebaberize()}-{counter}";
            goto Start;
        }

        public static string ToSlug(this string text, GraphContext context)
        {
            var counter = 0;
            var slug = text.Pascalize().Kebaberize();
            Start:

            var card = context.Cards.SingleOrDefault(c => c.Slug == slug);
            if (card == null) return slug;

            counter += counter;
            slug = $"{text.Pascalize().Kebaberize()}-{counter}";
            goto Start;
        }

        public static string RemoveDiacritics(this string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return s.Normalize(NormalizationForm.FormC);
        }
    }
}
