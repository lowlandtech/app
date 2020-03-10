using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using System;

namespace Spotacard.Features.Attributes
{
    public class AttributeData : ISeeder
    {
        public const string CardId = "e7be3b53-4ddb-406f-8c90-a0486087f733";

        public const string CardAttributeId1 = "7ed5fc9f-e539-4841-aa54-70daffcacff6";
        public const string CardAttributeId2 = "1e633ff7-7688-4ecb-b85e-3c2242921af2";

        public static readonly Card Card = new Card
        {
            Id = Guid.Parse(CardId),
            Title = "Item 1",
            Type = CardTypes.Todo
        };

        public static readonly CardAttribute CardAttribute1 = new CardAttribute
        {
            Index = 0,
            Id = Guid.Parse(CardAttributeId1),
            Name = "Item 1",
            Type = "string",
            Value = "Hello 1",
            Card = Card,
            CardId = new Guid(CardId)
        };

        public static readonly CardAttribute CardAttribute2 = new CardAttribute
        {
            Index = 1,
            Id = Guid.Parse(CardAttributeId2),
            Name = "Item 2",
            Type = "string",
            Value = "Hello 2",
            Card = Card,
            CardId = new Guid(CardId)
        };

        private readonly GraphContext _context;

        public AttributeData(GraphContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            _context.Cards.Add(Card);
            _context.Attributes.Add(CardAttribute1);
            _context.Attributes.Add(CardAttribute2);
            _context.SaveChanges();
        }
    }
}
