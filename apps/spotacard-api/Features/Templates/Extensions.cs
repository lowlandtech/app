using System.Linq;
using EnsureThat;
using Spotacard.Domain;
using Spotacard.Features.Templates.Enums;

namespace Spotacard.Features.Templates
{
    internal static class Extensions
    {
        public static object Get(this Card card, TemplateFields field)
        {
            Ensure.That(card).IsNotNull();
            Ensure.That(card.CardAttributes).IsNotNull().HasItems();

            var attribute = card.CardAttributes.SingleOrDefault(a => a.Name == field.ToString());

            Ensure.That(attribute).IsNotNull();

            return attribute.Value;
        }
    }
}
