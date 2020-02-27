using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;

namespace Spotacard.Features.Cards
{
    public class CardTests
    {
        [Test]
        public void ShouldWriteCardToDatabase()
        {
            var fixture = new TestFixture();
            try
            {
                var graph = fixture.GetGraph();
                var service = new CardService(graph);
                service.Add("https://example.com");
                graph.SaveChanges();

                Assert.That(1, Is.EqualTo(graph.Cards.Count()));
                Assert.That("https://example.com", Is.EqualTo(graph.Cards.Single().Title));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void ShouldBeEmpty()
        {
            var fixture = new TestFixture();
            try
            {
                var graph = fixture.GetGraph();
                Assert.False(graph.Cards.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void ShouldBeRequiredName()
        {
            var fixture = new TestFixture();
            try
            {
                var graph = fixture.GetGraph();
                var card = new Card();
                graph.Cards.Add(card);
                Assert.Throws<DbUpdateException>(() => graph.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void ShouldGenerateCardId()
        {
            var fixture = new TestFixture();
            try
            {
                var graph = fixture.GetGraph();
                var card = new Card {Title = "TestCard"};
                graph.Cards.Add(card);
                graph.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(card.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void ShouldFindCardByName()
        {
            // In-memory database only exists while the connection is open
            var fixture = new TestFixture();

            try
            {
                var graph = fixture.GetGraph();

                graph.Cards.Add(new Card {Title = "https://example.com/cats"});
                graph.Cards.Add(new Card {Title = "https://example.com/catfish"});
                graph.Cards.Add(new Card {Title = "https://example.com/dogs"});
                graph.SaveChanges();

                var service = new CardService(graph);
                var result = service.Find("cat");
                Assert.That(2, Is.EqualTo(result.Count()));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
