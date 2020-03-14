using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Stacks.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks
{
    public class StackUnitTests
    {
        [Test]
        public async Task Expect_Create_Stack()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            var command = new Create.Command
            {
                Stack = new Create.StackData
                {
                    Name = "TestStack",
                    ContentId = TestCase8201.Content.Id
                },
                CardId = TestCase8201.StackCard.Id
            };

            var stack = await StackHelpers.CreateStack(fixture, command);

            Assert.That(stack, Is.Not.Null);
            Assert.That(stack.Name, Is.EqualTo(command.Stack.Name));
        }

        [Test]
        public async Task Expect_Edit_Stack()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            try
            {
                var command = new Create.Command
                {
                    Stack = new Create.StackData
                    {
                        Name = "TestStack",
                        ContentId = TestCase8201.Content.Id
                    },
                    CardId = TestCase8201.StackCard.Id
                };

                var created = await StackHelpers.CreateStack(fixture, command);
                var editCommand = new Edit.Command
                {
                    Stack = new Edit.StackData
                    {
                        Name = "Updated " + created.Name,
                        ContentId = TestCase8201.Content.Id
                    },
                    StackId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Stack.Name, Is.EqualTo(editCommand.Stack.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Stack()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase8201(context));
            var createCmd = new Create.Command
            {
                Stack = new Create.StackData
                {
                    Name = "TestStack",
                    ContentId = TestCase8201.Content.Id
                },
                CardId = TestCase8201.StackCard.Id
            };

            var created = await StackHelpers.CreateStack(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Stacks
                .Where(stack => stack.Id == deleteCmd.StackId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_Create_Stack_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var stack = new Stack();
                context.Stacks.Add(stack);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Stack_Should_Generate_Id()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            try
            {
                var context = fixture.GetContext();
                var stack = new Stack
                {
                    Name = "TestStack",
                    CardId = TestCase8201.StackCard.Id,
                    ContentId = TestCase8201.Content.Id
                };
                context.Stacks.Add(stack);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(stack.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
