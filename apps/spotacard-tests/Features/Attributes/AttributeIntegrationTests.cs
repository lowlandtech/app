using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Spotacard.Domain;

namespace Spotacard.Features.Attributes
{
    public class AttributeIntegrationTests
    {
        [Test]
        public async Task Expect_List_Attribute()
        {
            // Arrange
            var fixture = new TestFixture(graph => new AttributeData(graph));
            // Act
            var client = fixture.Application.CreateClient();
            var response = await client.GetAsync($"cards/{AttributeData.CardId}/attributes");

            Assert.That(response.IsSuccessStatusCode, Is.True);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AttributesEnvelope>(json);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Attributes, Is.Not.Null);
            Assert.That(result.Attributes.Count, Is.EqualTo(2));
            Assert.That(result.Attributes.Find(attribute => attribute.Id == AttributeData.CardAttribute1.Id), Is.Not.Null);
            Assert.That(result.Attributes.Find(attribute => attribute.Id == AttributeData.CardAttribute2.Id), Is.Not.Null);
        }

        [Test]
        public async Task Expect_Create_Attribute()
        {
            // Arrange
            var uri = new Uri($"cards/{AttributeData.CardId}/attributes", UriKind.Relative);
            var fixture = new TestFixture(graph => new AttributeData(graph));
            var command = new Create.Command
            {
                Attribute = new Create.AttributeData
                {
                    Name = "Description",
                    Type = "string",
                    Value = "Body of the test attribute"
                },
                CardId = new Guid(AttributeData.CardId)
            };

            // Act
            var client = fixture.CreateClient();

            var response = await client.PostAsync(uri, fixture.Content(command));
            Assert.That(response.IsSuccessStatusCode, Is.True);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AttributeEnvelope>(json);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Attribute, Is.Not.Null);
            Assert.That(result.Attribute.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.Attribute.Index, Is.EqualTo(2));
            Assert.That(result.Attribute.Name, Is.EqualTo("Description"));
            Assert.That(result.Attribute.Type, Is.EqualTo("string"));
            Assert.That(result.Attribute.Value, Is.EqualTo("Body of the test attribute"));
        }
    }
}
