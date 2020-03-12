using NUnit.Framework;
using Spotacard.Features.Widgets.Commands;
using Spotacard.Features.Widgets.Types;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Widgets
{
    public class WidgetIntegrationTests
    {
        [Test]
        public async Task Expect_List_Widget()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            try
            {
                // Arrange
                var uri = new Uri("widgets", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<WidgetsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Widgets.Count, Is.EqualTo(10));
                Assert.That(result.Widgets.Find(widget => widget.Id == TestCase1101.Widget1.Id), Is.Not.Null);
                Assert.That(result.Widgets.Find(widget => widget.Id == TestCase1101.Widget2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Widget_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            try
            {
                // Arrange
                var uri = new Uri($"widgets/{TestCase1101.Widget1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<WidgetEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Widget.Id, Is.EqualTo(TestCase1101.Widget1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Widget()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            try
            {
                // Arrange
                var uri = new Uri($"widgets", UriKind.Relative);
                var command = new Create.Command
                {
                    Widget = new Create.WidgetData
                    {
                        Name = "TestWidget",
                    },
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<WidgetEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Widget, Is.Not.Null);
                Assert.That(result.Widget.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Widget.Name, Is.EqualTo("TestWidget"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Widget()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"widgets/{TestCase1101.Widget1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Widget = new Edit.WidgetData
                    {
                        Name = updated + TestCase1101.Widget1.Name
                    },
                    WidgetId = TestCase1101.Widget1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<WidgetEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Widget, Is.Not.Null);
                Assert.That(result.Widget.Name, Is.EqualTo(updated + TestCase1101.Widget1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Widget_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"widgets/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Widget()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            try
            {
                // Arrange
                var uri = new Uri($"widgets/{TestCase1101.Widget1.Id}", UriKind.Relative);
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
