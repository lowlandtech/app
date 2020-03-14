using NUnit.Framework;
using Spotacard.Features.Stacks.Commands;
using Spotacard.Features.Stacks.Types;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks
{
    public class StackIntegrationTests
    {
        [Test]
        public async Task Expect_List_Stack()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            try
            {
                // Arrange
                var uri = new Uri("stacks", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<StacksEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Stacks.Find(stack => stack.Id == TestCase8201.Stack1.Id), Is.Not.Null);
                Assert.That(result.Stacks.Find(stack => stack.Id == TestCase8201.Stack2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Stack_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            try
            {
                // Arrange
                var uri = new Uri($"stacks/{TestCase8201.Stack1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<StackEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Stack.Id, Is.EqualTo(TestCase8201.Stack1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Stack()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            try
            {
                // Arrange
                var uri = new Uri($"stacks", UriKind.Relative);
                var command = new Create.Command
                {
                    Stack = new Create.StackData
                    {
                        Name = "TestStack",
                        ContentId = TestCase8201.Content.Id
                    },
                    CardId = TestCase8201.StackCard.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<StackEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Stack, Is.Not.Null);
                Assert.That(result.Stack.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Stack.Name, Is.EqualTo("TestStack"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Stack()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"stacks/{TestCase8201.Stack1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Stack = new Edit.StackData
                    {
                        Name = updated + TestCase8201.Stack1.Name,
                        ContentId = TestCase8201.Content.Id,
                    },
                    StackId = TestCase8201.Stack1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<StackEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Stack, Is.Not.Null);
                Assert.That(result.Stack.Name, Is.EqualTo(updated + TestCase8201.Stack1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Stack_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"stacks/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Stack()
        {
            var fixture = new TestFixture(context => new TestCase8201(context));
            try
            {
                // Arrange
                var uri = new Uri($"stacks/{TestCase8201.Stack1.Id}", UriKind.Relative);
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
