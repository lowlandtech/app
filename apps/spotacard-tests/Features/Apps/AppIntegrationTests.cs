using NUnit.Framework;
using Spotacard.Features.Apps.Commands;
using Spotacard.Features.Apps.Types;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Apps
{
    public class AppIntegrationTests
    {
        [Test]
        public async Task Expect_List_App()
        {
            var fixture = new TestFixture(context => new TestCase1001(context));
            try
            {
                // Arrange
                var uri = new Uri("apps", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<AppsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Apps.Count, Is.EqualTo(10));
                Assert.That(result.Apps.Find(app => app.Id == TestCase1001.App1.Id), Is.Not.Null);
                Assert.That(result.Apps.Find(app => app.Id == TestCase1001.App2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_App_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase1001(context));
            try
            {
                // Arrange
                var uri = new Uri($"apps/{TestCase1001.App1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<AppEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.App.Id, Is.EqualTo(TestCase1001.App1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_App()
        {
            var fixture = new TestFixture(context => new TestCase1001(context));
            try
            {
                // Arrange
                var uri = new Uri($"apps", UriKind.Relative);
                var command = new Create.Command
                {
                    App = new Create.AppData
                    {
                        Name = "TestApp",
                        Organization = "Organization",
                        Prefix = "TA",
                        Namingspace = "Organization.Test.App"
                    }
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<AppEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.App, Is.Not.Null);
                Assert.That(result.App.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.App.Name, Is.EqualTo("TestApp"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_App()
        {
            var fixture = new TestFixture(context => new TestCase1001(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"apps/{TestCase1001.App1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    App = new Edit.AppData
                    {
                        Name = updated + TestCase1001.App1.Name,
                        Organization = updated + TestCase1001.App1.Organization,
                        Prefix = updated + TestCase1001.App1.Prefix,
                        Namingspace = updated + TestCase1001.App1.Namingspace
                    },
                    AppId = TestCase1001.App1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<AppEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.App, Is.Not.Null);
                Assert.That(result.App.Name, Is.EqualTo(updated + TestCase1001.App1.Name));
                Assert.That(result.App.Organization, Is.EqualTo(updated + TestCase1001.App1.Organization));
                Assert.That(result.App.Prefix, Is.EqualTo(updated + TestCase1001.App1.Prefix));
                Assert.That(result.App.Namingspace, Is.EqualTo(updated + TestCase1001.App1.Namingspace));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_App_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1001(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"apps/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_App()
        {
            var fixture = new TestFixture(context => new TestCase1001(context));
            try
            {
                // Arrange
                var uri = new Uri($"apps/{TestCase1001.App1.Id}", UriKind.Relative);
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
