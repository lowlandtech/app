using System;
using System.Collections.Generic;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;

namespace Spotacard.TestCases
{
    public class TestCase80 : ISeeder
    {
        public const string Id = "6cc277d5-253e-48e0-8a9a-8fe3cae17e5b";

        public static readonly Card Card = new Card
        {
            Id = Guid.Parse(Id),
            Title = "Hello World",
            Slug = "hello-world",
            Description = "Hello, @Model.Title",
            Type = CardTypes.Template,
            CardAttributes = new List<CardAttribute>
            {
                new CardAttribute
                {
                    Index = 0,
                    Name = "Type",
                    Value = "0",
                    Type = "Spotacard.Features.Templates.Enums.types"
                },
                new CardAttribute
                {
                    Index = 1,
                    Name = "FileName",
                    Value = "d://repositories//tmp//@Model.Title",
                    Type = "string"
                }
            }
        };

        private readonly GraphContext _context;

        public TestCase80(GraphContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            _context.Cards.Add(Card.Copy());
            _context.Cards.Add(new Card
            {
                Id = new Guid("bee3223a-276d-40a5-ad25-d8ac3967132e"),
                Title = "John Doe",
                Type = CardTypes.App
            });
            _context.SaveChanges();
        }
    }
}
