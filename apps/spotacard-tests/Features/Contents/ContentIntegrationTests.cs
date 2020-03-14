using NUnit.Framework;
using Spotacard.Features.Contents.Commands;
using Spotacard.Features.Contents.Types;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Contents
{
    public class ContentIntegrationTests
    {
        [Test]
        public async Task Expect_List_Content()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            try
            {
                // Arrange
                var uri = new Uri("contents", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<ContentsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Contents.Find(content => content.Id == TestCase8101.Content1.Id), Is.Not.Null);
                Assert.That(result.Contents.Find(content => content.Id == TestCase8101.Content2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Content_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            try
            {
                // Arrange
                var uri = new Uri($"contents/{TestCase8101.Content1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<ContentEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Content.Id, Is.EqualTo(TestCase8101.Content1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Content()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            try
            {
                // Arrange
                var uri = new Uri($"contents", UriKind.Relative);
                var command = new Create.Command
                {
                    Content = new Create.ContentData
                    {
                        Name = "TestContent",
                        Text = "TestContentText",
                    },
                    CardId = TestCase8101.Template.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<ContentEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Content, Is.Not.Null);
                Assert.That(result.Content.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Content.Name, Is.EqualTo("TestContent"));
                Assert.That(result.Content.Text, Is.EqualTo("TestContentText"));
                Assert.That(result.Content.Index, Is.EqualTo(0));
                Assert.That(result.Content.CardId, Is.EqualTo(TestCase8101.Template.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Content()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"contents/{TestCase8101.Content1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Content = new Edit.ContentData
                    {
                        Name = updated + TestCase8101.Content1.Name
                    },
                    ContentId = TestCase8101.Content1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<ContentEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Content, Is.Not.Null);
                Assert.That(result.Content.Name, Is.EqualTo(updated + TestCase8101.Content1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Content_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"contents/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Content()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            try
            {
                // Arrange
                var uri = new Uri($"contents/{TestCase8101.Content1.Id}", UriKind.Relative);
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
