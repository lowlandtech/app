using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.TestServer;

namespace Spotacard.Features.Cards
{
    public class CardIntegrationTests
    {
        [Test]
        public async Task ShouldGetCardList()
        {
            // Arrange
            var factory = TestServerFixture.Get<Startup>(graph => new CardData(graph));

            // Act
            var client = factory.CreateClient();
            var response = await client.GetAsync("cards");
            var json = await response.Content.ReadAsStringAsync();
            var cards = JsonConvert.DeserializeObject<List<Card>>(json);

            // Assert
            Assert.That(cards.Count, Is.EqualTo(2));
            Assert.That(cards.Find(card => card.Id == CardData.FirstItem.Id), Is.Not.Null);
            Assert.That(cards.Find(card => card.Id == CardData.SecondItem.Id), Is.Not.Null);
        }

        [Test]
        public async Task ShouldGetCardById()
        {
            // Arrange
            var factory = TestServerFixture.Get<Startup>(graph => new CardData(graph));

            // Act
            var client = factory.CreateClient();
            var response = await client.GetAsync($"cards/id/{CardData.FirstItemId}");
            var json = await response.Content.ReadAsStringAsync();
            var card = JsonConvert.DeserializeObject<Card>(json);

            // Assert
            Assert.That(card.Id, Is.EqualTo(CardData.FirstItem.Id));
        }

        [Test]
        public async Task ShouldPostNewCard()
        {
            // Arrange
            var factory = TestServerFixture.Get<Startup>(graph => new CardData(graph));
            var card = new Card
            {
                Title = "Test3"
            };
            var content = JsonConvert.SerializeObject(card);
            var buffer = Encoding.UTF8.GetBytes(content);
            var bytes = new ByteArrayContent(buffer);
            bytes.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var client = factory.CreateClient();
            var response = await client.PostAsync("cards", bytes);
            var json = await response.Content.ReadAsStringAsync();
            var added = JsonConvert.DeserializeObject<Card>(json);

            // Assert
            Assert.That(added, Is.Not.Null);
            Assert.That(added.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(added.Title, Is.EqualTo("Test3"));
        }

        [Test]
        public async Task ShouldPutUpdatedCard()
        {
            // Arrange
            var factory = TestServerFixture.Get<Startup>(graph => new CardData(graph));
            var card = CardData.FirstItem;
            card.Title = "Test4";
            var content = JsonConvert.SerializeObject(card);
            var buffer = Encoding.UTF8.GetBytes(content);
            var bytes = new ByteArrayContent(buffer);
            bytes.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var client = factory.CreateClient();
            await client.PutAsync($"cards/{card.Id}", bytes);
            var response = await client.GetAsync($"cards/id/{card.Id}");
            var json = await response.Content.ReadAsStringAsync();
            var updated = JsonConvert.DeserializeObject<Card>(json);

            // Assert
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.Title, Is.EqualTo("Test4"));
        }

        [Test]
        public async Task ShouldReturnNotFoundPut()
        {
            // Arrange
            var factory = TestServerFixture.Get<Startup>(graph => new CardData(graph));
            var card = new Card
            {
                Id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d"),
                Title = "Test4"
            };
            var content = JsonConvert.SerializeObject(card);
            var buffer = Encoding.UTF8.GetBytes(content);
            var bytes = new ByteArrayContent(buffer);
            bytes.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var client = factory.CreateClient();
            await client.PutAsync($"cards/{card.Id}", bytes);
            var response = await client.GetAsync($"cards/id/{card.Id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task ShouldDeleteCard()
        {
            // Arrange
            var factory = TestServerFixture.Get<Startup>(graph => new CardData(graph));
            var client = factory.CreateClient();

            // Act
            await client.DeleteAsync($"cards/{CardData.FirstItem.Id}");
            var response = await client.GetAsync($"cards/id/{CardData.FirstItem.Id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
