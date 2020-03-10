using System;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Cards
{
    public class CardData : IActivity
    {
        public const string FirstItemId = "312658D1-8146-42E3-B57B-360427182811";
        public const string SecondItemId = "64C7E3F5-74F9-4540-9B12-BC7AFBCC7CE6";

        public static readonly Card FirstItem = new Card
        {
            Id = Guid.Parse(FirstItemId),
            Title = "Item 1",
            Slug = "item-1",
            Type = CardTypes.Todo
        };

        public static readonly Card SecondItem = new Card
        {
            Id = Guid.Parse(SecondItemId),
            Title = "Item 2",
            Slug = "item-2",
            Type = CardTypes.Todo
        };

        private readonly GraphContext _context;

        public CardData(GraphContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            _context.Cards.Add(FirstItem.Copy());
            _context.Cards.Add(SecondItem.Copy());
            _context.SaveChanges();
        }
    }
}
