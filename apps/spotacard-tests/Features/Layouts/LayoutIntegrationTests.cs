using NUnit.Framework;
using Spotacard.Features.Layouts.Commands;
using Spotacard.Features.Layouts.Types;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Layouts
{
    public class LayoutIntegrationTests
    {
        [Test]
        public async Task Expect_List_Layout()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            try
            {
                // Arrange
                var uri = new Uri("layouts", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<LayoutsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Layouts.Count, Is.EqualTo(10));
                Assert.That(result.Layouts.Find(layout => layout.Id == TestCase1201.Layout1.Id), Is.Not.Null);
                Assert.That(result.Layouts.Find(layout => layout.Id == TestCase1201.Layout2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Layout_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            try
            {
                // Arrange
                var uri = new Uri($"layouts/{TestCase1201.Layout1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<LayoutEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Layout.Id, Is.EqualTo(TestCase1201.Layout1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Layout()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            try
            {
                // Arrange
                var uri = new Uri($"layouts", UriKind.Relative);
                var command = new Create.Command
                {
                    Layout = new Create.LayoutData
                    {
                        Name = "TestLayout",
                        Items = "item1,item2"
                    },
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<LayoutEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Layout, Is.Not.Null);
                Assert.That(result.Layout.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Layout.Name, Is.EqualTo("TestLayout"));
                Assert.That(result.Layout.Items, Is.EqualTo("item1,item2"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Layout()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"layouts/{TestCase1201.Layout1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Layout = new Edit.LayoutData
                    {
                        Name = updated + TestCase1201.Layout1.Name
                    },
                    LayoutId = TestCase1201.Layout1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<LayoutEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Layout, Is.Not.Null);
                Assert.That(result.Layout.Name, Is.EqualTo(updated + TestCase1201.Layout1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Layout_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"layouts/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Layout()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            try
            {
                // Arrange
                var uri = new Uri($"layouts/{TestCase1201.Layout1.Id}", UriKind.Relative);
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
