using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Apps;
using Spotacard.Features.Pages.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Pages
{
    public class PageUnitTests
    {
        [Test]
        public async Task Expect_Create_Page()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            var command = new Create.Command
            {
                Page = new Create.PageData
                {
                    Name = "TestPage"
                },
                AppId = TestCase1001.App0.Id
            };

            var page = await PageHelpers.CreatePage(fixture, command);

            Assert.That(page, Is.Not.Null);
            Assert.That(page.Name, Is.EqualTo(command.Page.Name));
        }

        [Test]
        public async Task Expect_Edit_Page()
        {
            var fixture = new TestFixture(context => new TestCase1003(context));
            try
            {
                var command = new Create.Command
                {
                    Page = new Create.PageData
                    {
                        Name = "TestPage"
                    },
                    AppId = TestCase1001.App0.Id
                };

                var created = await PageHelpers.CreatePage(fixture, command);
                var editCommand = new Edit.Command
                {
                    Page = new Edit.PageData
                    {
                        Name = "Updated " + created.Name
                    },
                    PageId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Page.Name, Is.EqualTo(editCommand.Page.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Page()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase1003(context));
            var createCmd = new Create.Command
            {
                Page = new Create.PageData
                {
                    Name = "TestPage"
                },
                AppId = TestCase1001.App0.Id
            };

            var created = await PageHelpers.CreatePage(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Pages
                .Where(page => page.Id == deleteCmd.PageId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_List_Page_To_Be_Empty()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.False(context.Pages.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Page_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var page = new Page();
                context.Pages.Add(page);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Page_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var page = new Page
                {
                    Name = "TestPage",
                    App = TestCase1001.App0,
                    AppId = TestCase1001.App0.Id
                };
                context.Pages.Add(page);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(page.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
