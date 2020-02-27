using Newtonsoft.Json;
using NUnit.Framework;
using Spotacard.Domain;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
    public class AttributeIntegrationTests
    {
        [Test]
        public async Task Expect_List_Attribute()
        {
            // Arrange
            var fixture = new SliceFixture(graph => new AttributeData(graph));
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
            var fixture = new SliceFixture(graph => new AttributeData(graph));
            var attribute = new CardAttribute
            {
                Name = "Description",
                Type = "string",
                Value = "Body of the test attribute"
            };
            var content = JsonConvert.SerializeObject(attribute);
            var buffer = Encoding.UTF8.GetBytes(content);
            var bytes = new ByteArrayContent(buffer);
            bytes.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var client = fixture.Application.CreateClient();
            var response = await client.PostAsync($"cards/{AttributeData.CardId}/attributes", bytes);

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
