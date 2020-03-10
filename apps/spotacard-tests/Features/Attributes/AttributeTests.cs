using NUnit.Framework;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
    public class AttributeTests
    {
        [Test]
        public async Task Expect_List_Attribute_By_Name()
        {
            var fixture = new TestFixture(context => new TestCase1Data(context));
            var attribute = TestCase1Data.Id.Copy();
            var graph = fixture.GetGraph();
            try
            {
                var result = await graph.GetCardAttribute(attribute.CardId, attribute.Name);

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(attribute.Value));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Attribute_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1Data(context));
            var graph = fixture.GetGraph();
            try
            {
                var result = await graph.GetCardAttribute(TestCase1Data.Card.Id, "Text");

                Assert.That(result, Is.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Attribute_Not_Found_Card()
        {
            var fixture = new TestFixture(context => new TestCase1Data(context));
            var graph = fixture.GetGraph();
            try
            {
                var result = await graph.GetCardAttribute(Guid.NewGuid(), "Text");
                Assert.That(result, Is.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Attribute()
        {
            var fixture = new TestFixture(context => new TestCase1Data(context));
            var graph = fixture.GetGraph();
            try
            {
                var attributes = new List<CardAttribute>();
                await graph.AddCard(CardTypes.Todo, "my todo", attributes);
                var result = await graph.GetCardAttribute(Guid.NewGuid(), "Text");
                Assert.That(result, Is.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }

    public class TestCase1Data : ISeeder
    {
        public const string CardId = "9be1a7ff-a8a1-4fef-bf8e-c41ce0f669a4";
        public const string CardAttributeId1 = "25414f35-1bcd-4f9a-a328-0d5619c899a6";
        public const string CardAttributeId2 = "c7659310-0983-4f5e-aed8-4f17deaef1ac";

        public static readonly Card Card = new Card
        {
            Id = Guid.Parse(CardId),
            Title = "existing 1",
            Type = CardTypes.Todo,
            Slug = "existing-1"
        };

        public static readonly CardAttribute Id = new CardAttribute
        {
            Index = 0,
            Id = Guid.Parse(CardAttributeId1),
            Name = "id",
            Type = "guid",
            Value = "bee4f3aa-9a18-44df-9925-cd9944740f4f",
            Card = Card,
            CardId = new Guid(CardId)
        };

        public static readonly CardAttribute Name = new CardAttribute
        {
            Index = 1,
            Id = Guid.Parse(CardAttributeId2),
            Name = "name",
            Type = "string",
            Value = "test1",
            Card = Card,
            CardId = new Guid(CardId)
        };

        private readonly GraphContext _context;

        public TestCase1Data(GraphContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            _context.Cards.Add(Card);
            _context.Attributes.Add(Id);
            _context.Attributes.Add(Name);
            _context.SaveChanges();
        }
    }
}
