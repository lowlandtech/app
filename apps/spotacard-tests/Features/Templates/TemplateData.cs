using System;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Templates
{
    public class TemplateData : ISeeder
    {
        public const string Id = "6cc277d5-253e-48e0-8a9a-8fe3cae17e5b";

        public static readonly Card Card = new Card
        {
            Id = Guid.Parse(Id),
            Title = "Hello World",
            Slug = "hello-world",
            Description = "Hello, @Model.Name",
            Type = ContractTypes.Template
        };

        private readonly GraphContext _context;

        public TemplateData(GraphContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            _context.Cards.Add(Card.Copy());
            _context.SaveChanges();
        }
    }
}
