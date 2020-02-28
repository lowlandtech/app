using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Cards
{
    public class CardIntegrationTests
    {
        [Test]
        public async Task ShouldGetCardList()
        {
            var fixture = new TestFixture(graph => new CardData(graph));
            try
            {
                // Arrange
                var uri = new Uri("cards", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<CardsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Cards.Count, Is.EqualTo(2));
                Assert.That(result.Cards.Find(card => card.Id == CardData.FirstItem.Id), Is.Not.Null);
                Assert.That(result.Cards.Find(card => card.Id == CardData.SecondItem.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task ShouldGetCardById()
        {
            var fixture = new TestFixture(graph => new CardData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards/id/{CardData.FirstItemId}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<CardEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Card.Id, Is.EqualTo(CardData.FirstItem.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task ShouldPostNewCard()
        {
            var fixture = new TestFixture(graph => new CardData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards", UriKind.Relative);
                var command = new Create.Command
                {
                    Card = new Create.CardData
                    {
                        Title = "Test card",
                        Description = "Description of the test card",
                        Body = "Body of the test card",
                        TagList = "tag1,tag2"
                    }
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<CardEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Card, Is.Not.Null);
                Assert.That(result.Card.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Card.Title, Is.EqualTo("Test card"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task ShouldPutUpdatedCard()
        {
            var fixture = new TestFixture(graph => new CardData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards/{CardData.FirstItem.Slug}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Card = new Edit.CardData
                    {
                        Title = "Updated " + CardData.FirstItem.Title,
                        Description = "Updated " + CardData.FirstItem.Description,
                        Body = "Updated " + CardData.FirstItem.Body
                    },
                    Slug = CardData.FirstItem.Slug
                };
                // remove the first tag and add a new tag
                command.Card.TagList = "tag1,tag2,tag3";

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<CardEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Card, Is.Not.Null);
                Assert.That(result.Card.Title, Is.EqualTo("Updated " + CardData.FirstItem.Title));
                Assert.That(result.Card.Description, Is.EqualTo("Updated " + CardData.FirstItem.Description));
                Assert.That(result.Card.Body, Is.EqualTo("Updated " + CardData.FirstItem.Body));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task ShouldReturnNotFoundPut()
        {
            var fixture = new TestFixture(graph => new CardData(graph));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"cards/id/{id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task ShouldDeleteCard()
        {
            var fixture = new TestFixture(graph => new CardData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards/{CardData.FirstItem.Id}", UriKind.Relative);
                var client = fixture.CreateClient();
                // Act
                var response = await client.DeleteAsync(uri);
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
            finally
            {
                fixture.Dispose();
            }

        }
    }
}
