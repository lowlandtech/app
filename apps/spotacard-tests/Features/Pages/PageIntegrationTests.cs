using NUnit.Framework;
using Spotacard.Features.Pages.Commands;
using Spotacard.Features.Pages.Types;
using System;
using System.Net;
using System.Threading.Tasks;
using Spotacard.Features.Apps;

namespace Spotacard.Features.Pages
{
    public class PageIntegrationTests
    {
        [Test]
        public async Task Expect_List_Page()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            try
            {
                // Arrange
                var uri = new Uri("pages", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<PagesEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Pages.Count, Is.EqualTo(10));
                Assert.That(result.Pages.Find(page => page.Id == TestCase1003.Page1.Id), Is.Not.Null);
                Assert.That(result.Pages.Find(page => page.Id == TestCase1003.Page2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Page_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            try
            {
                // Arrange
                var uri = new Uri($"pages/{TestCase1003.Page1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<PageEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Page.Id, Is.EqualTo(TestCase1003.Page1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Page()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            try
            {
                // Arrange
                var uri = new Uri($"pages", UriKind.Relative);
                var command = new Create.Command
                {
                    Page = new Create.PageData
                    {
                        Name = "TestPage",
                    },
                    AppId = TestCase1001.App0.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<PageEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Page, Is.Not.Null);
                Assert.That(result.Page.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Page.Name, Is.EqualTo("TestPage"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Page()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"pages/{TestCase1003.Page1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Page = new Edit.PageData
                    {
                        Name = updated + TestCase1003.Page1.Name
                    },
                    PageId = TestCase1003.Page1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<PageEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Page, Is.Not.Null);
                Assert.That(result.Page.Name, Is.EqualTo(updated + TestCase1003.Page1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Page_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"pages/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Page()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            try
            {
                // Arrange
                var uri = new Uri($"pages/{TestCase1003.Page1.Id}", UriKind.Relative);
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
