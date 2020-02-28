using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
    public class AttributeIntegrationTests
    {
        [Test]
        public async Task Expect_List_Attribute()
        {
            var fixture = new TestFixture(graph => new AttributeData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards/{AttributeData.CardId}/attributes", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<AttributesEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Attributes, Is.Not.Null);
                Assert.That(result.Attributes.Find(attribute => attribute.Id == AttributeData.CardAttribute1.Id), Is.Not.Null);
                Assert.That(result.Attributes.Find(attribute => attribute.Id == AttributeData.CardAttribute2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Attribute()
        {
            var fixture = new TestFixture(graph => new AttributeData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards/{AttributeData.CardId}/attributes", UriKind.Relative);
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
                var result = await fixture.Get<AttributeEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Attribute, Is.Not.Null);
                Assert.That(result.Attribute.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Attribute.Index, Is.EqualTo(2));
                Assert.That(result.Attribute.Name, Is.EqualTo("Description"));
                Assert.That(result.Attribute.Type, Is.EqualTo("string"));
                Assert.That(result.Attribute.Value, Is.EqualTo("Body of the test attribute"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Attribute()
        {
            var fixture = new TestFixture(graph => new AttributeData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards/{AttributeData.CardId}/attributes/{AttributeData.CardAttributeId1}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Attribute = new Edit.AttributeData
                    {
                        Name = "Updated " + AttributeData.CardAttribute1.Name,
                        Type = "Updated" + AttributeData.CardAttribute1.Type,
                        Value = "Updated" + AttributeData.CardAttribute1.Value,
                        CardId = new Guid(AttributeData.CardId)
                    },
                    Id = AttributeData.CardAttribute1.Id
                };
                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<AttributeEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Attribute.Name, Is.EqualTo(command.Attribute.Name));
                Assert.That(result.Attribute.Type, Is.EqualTo(command.Attribute.Type));
                Assert.That(result.Attribute.Value, Is.EqualTo(command.Attribute.Value));
                Assert.That(result.Attribute.Index, Is.EqualTo(command.Attribute.Index));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Attribute()
        {
            var fixture = new TestFixture(graph => new AttributeData(graph));
            try
            {
                // Arrange
                var uri = new Uri($"cards/{AttributeData.CardId}/attributes/{AttributeData.CardAttributeId1}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.DeleteAsync(uri);
                // Assert
                Assert.That(response.IsSuccessStatusCode, Is.True);
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
