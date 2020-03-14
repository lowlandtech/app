using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Contents.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Contents
{
    public class ContentUnitTests
    {
        [Test]
        public async Task Expect_Create_Content()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            var command = new Create.Command
            {
                Content = new Create.ContentData
                {
                    Name = "TestContent"
                },
                CardId = TestCase8101.Template.Id
            };

            var content = await ContentHelpers.CreateContent(fixture, command);

            Assert.That(content, Is.Not.Null);
            Assert.That(content.Name, Is.EqualTo(command.Content.Name));
        }

        [Test]
        public async Task Expect_Edit_Content()
        {
            var fixture = new TestFixture(context => new TestCase8101(context));
            try
            {
                var command = new Create.Command
                {
                    Content = new Create.ContentData
                    {
                        Name = "TestContent"
                    },
                    CardId = TestCase8101.Template.Id
                };

                var created = await ContentHelpers.CreateContent(fixture, command);
                var editCommand = new Edit.Command
                {
                    Content = new Edit.ContentData
                    {
                        Name = "Updated " + created.Name
                    },
                    ContentId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Content.Name, Is.EqualTo(editCommand.Content.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Content()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase8101(context));
            var createCmd = new Create.Command
            {
                Content = new Create.ContentData
                {
                    Name = "TestContent"
                },
                CardId = TestCase8101.Template.Id
            };

            var created = await ContentHelpers.CreateContent(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Contents
                .Where(content => content.Id == deleteCmd.ContentId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_Create_Content_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var content = new Content();
                context.Contents.Add(content);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Content_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var content = new Content
                {
                    Name = "TestContent",
                    Card = TestCase8101.Template,
                    CardId = TestCase8101.Template.Id
                };
                context.Contents.Add(content);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(content.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
