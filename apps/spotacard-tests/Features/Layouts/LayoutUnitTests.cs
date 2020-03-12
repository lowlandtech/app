using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Layouts.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Layouts
{
    public class LayoutUnitTests
    {
        [Test]
        public async Task Expect_Create_Layout()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            var command = new Create.Command
            {
                Layout = new Create.LayoutData
                {
                    Name = "TestLayout"
                },
            };

            var layout = await LayoutHelpers.CreateLayout(fixture, command);

            Assert.That(layout, Is.Not.Null);
            Assert.That(layout.Name, Is.EqualTo(command.Layout.Name));
        }

        [Test]
        public async Task Expect_Edit_Layout()
        {
            var fixture = new TestFixture(context => new TestCase1201(context));
            try
            {
                var command = new Create.Command
                {
                    Layout = new Create.LayoutData
                    {
                        Name = "TestLayout"
                    },
                };

                var created = await LayoutHelpers.CreateLayout(fixture, command);
                var editCommand = new Edit.Command
                {
                    Layout = new Edit.LayoutData
                    {
                        Name = "Updated " + created.Name
                    },
                    LayoutId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Layout.Name, Is.EqualTo(editCommand.Layout.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Layout()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase1201(context));
            var createCmd = new Create.Command
            {
                Layout = new Create.LayoutData
                {
                    Name = "TestLayout"
                },
            };

            var created = await LayoutHelpers.CreateLayout(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Layouts
                .Where(layout => layout.Id == deleteCmd.LayoutId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_List_Layout_To_Be_Empty()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.False(context.Layouts.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Layout_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var layout = new Layout();
                context.Layouts.Add(layout);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Layout_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var layout = new Layout
                {
                    Name = "TestLayout",
                };
                context.Layouts.Add(layout);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(layout.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
