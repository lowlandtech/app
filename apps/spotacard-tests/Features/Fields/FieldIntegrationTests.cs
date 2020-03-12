using NUnit.Framework;
using Spotacard.Features.Fields.Commands;
using Spotacard.Features.Fields.Types;
using Spotacard.Features.Tables;
using System;
using System.Net;
using System.Threading.Tasks;
using Spotacard.Core.Enums;

namespace Spotacard.Features.Fields
{
    public class FieldIntegrationTests
    {
        [Test]
        public async Task Expect_List_Field()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            try
            {
                // Arrange
                var uri = new Uri("fields", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<FieldsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Fields.Find(field => field.Id == TestCase1004.Field0.Id), Is.Not.Null);
                Assert.That(result.Fields.Find(field => field.Id == TestCase1004.Field2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Field_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            try
            {
                // Arrange
                var uri = new Uri($"fields/{TestCase1004.Field1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<FieldEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Field.Id, Is.EqualTo(TestCase1004.Field1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Field()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            try
            {
                // Arrange
                var uri = new Uri($"fields", UriKind.Relative);
                var command = new Create.Command
                {
                    Field = new Create.FieldData
                    {
                        Name = "TestField",
                        Type = FieldTypes.String,

                    },
                    TableId = TestCase1002.Table0.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<FieldEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Field, Is.Not.Null);
                Assert.That(result.Field.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Field.Name, Is.EqualTo("TestField"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Field()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"fields/{TestCase1004.Field1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Field = new Edit.FieldData
                    {
                        Name = updated + TestCase1004.Field1.Name
                    },
                    FieldId = TestCase1004.Field1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<FieldEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Field, Is.Not.Null);
                Assert.That(result.Field.Name, Is.EqualTo(updated + TestCase1004.Field1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Field_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"fields/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Field()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            try
            {
                // Arrange
                var uri = new Uri($"fields/{TestCase1004.Field1.Id}", UriKind.Relative);
                var client = fixture.CreateClient();
                // Act
                var response = await client.DeleteAsync(uri);
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
