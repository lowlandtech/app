using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Widgets.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Widgets
{
    public class WidgetUnitTests
    {
        [Test]
        public async Task Expect_Create_Widget()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            var command = new Create.Command
            {
                Widget = new Create.WidgetData
                {
                    Name = "TestWidget"
                },
            };

            var widget = await WidgetHelpers.CreateWidget(fixture, command);

            Assert.That(widget, Is.Not.Null);
            Assert.That(widget.Name, Is.EqualTo(command.Widget.Name));
        }

        [Test]
        public async Task Expect_Edit_Widget()
        {
            var fixture = new TestFixture(context => new TestCase1101(context));
            try
            {
                var command = new Create.Command
                {
                    Widget = new Create.WidgetData
                    {
                        Name = "TestWidget"
                    },
                };

                var created = await WidgetHelpers.CreateWidget(fixture, command);
                var editCommand = new Edit.Command
                {
                    Widget = new Edit.WidgetData
                    {
                        Name = "Updated " + created.Name
                    },
                    WidgetId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Widget.Name, Is.EqualTo(editCommand.Widget.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Widget()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase1101(context));
            var createCmd = new Create.Command
            {
                Widget = new Create.WidgetData
                {
                    Name = "TestWidget"
                },
            };

            var created = await WidgetHelpers.CreateWidget(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Widgets
                .Where(widget => widget.Id == deleteCmd.WidgetId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_List_Widget_To_Be_Empty()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.False(context.Widgets.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Widget_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var widget = new Widget();
                context.Widgets.Add(widget);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Widget_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var widget = new Widget
                {
                    Name = "TestWidget",
                };
                context.Widgets.Add(widget);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(widget.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
