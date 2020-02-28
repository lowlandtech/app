using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;

namespace Spotacard.Features.Attributes
{
    public class AttributeUnitTests
    {
        [Test]
        public async Task Expect_List_Attribute()
        {
            var fixture = new TestFixture();
            var card = fixture.CreateCard(new Card {Title = "Test Card 1"});
            fixture.CreateAttribute(new CardAttribute
            {
                Index = 0,
                Name = "Description",
                Type = "string",
                Value = "Body of the test attribute",
                Card = card,
                CardId = card.Id
            });
            fixture.CreateAttribute(new CardAttribute
            {
                Index = 1,
                Name = "Description",
                Type = "string",
                Value = "Body of the test attribute",
                Card = card,
                CardId = card.Id
            });

            var command = new List.Query(card.Id);

            var result = await AttributeHelpers.ListCardAttribute(fixture, command);

            Assert.That(result.Attributes, Is.Not.Null);
            Assert.That(result.Attributes.Count, Is.EqualTo(2));
        }


        [Test]
        public async Task Expect_Create_Attribute()
        {
            var fixture = new TestFixture();
            var card = fixture.CreateCard(new Card {Title = "Test Card 1"});

            var command = new Create.Command
            {
                Attribute = new Create.AttributeData
                {
                    Index = 0,
                    Name = "Description",
                    Type = "string",
                    Value = "Body of the test attribute"
                }
            };

            command.CardId = card.Id;
            var attribute = await AttributeHelpers.CreateCardAttribute(fixture, command);

            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Index, Is.EqualTo(command.Attribute.Index));
            Assert.That(attribute.Name, Is.EqualTo(command.Attribute.Name));
            Assert.That(attribute.Type, Is.EqualTo(command.Attribute.Type));
            Assert.That(attribute.Value, Is.EqualTo(command.Attribute.Value));
        }

        [Test]
        public async Task Expect_Edit_Attribute()
        {
            var fixture = new TestFixture();
            var card = fixture.CreateCard(new Card {Title = "Test Card 1"});
            var attribute = fixture.CreateAttribute(new CardAttribute
            {
                Index = 0,
                Name = "Description",
                Type = "string",
                Value = "Body of the test attribute",
                Card = card,
                CardId = card.Id
            });

            var editCommand = new Edit.Command
            {
                Attribute = new Edit.AttributeData
                {
                    Name = "Updated " + attribute.Name,
                    Type = "Updated" + attribute.Type,
                    Value = "Updated" + attribute.Value,
                    CardId = card.Id
                },
                Id = attribute.Id
            };

            // remove the first tag and add a new tag
            var context = fixture.GetContext();
            var handler = new Edit.Handler(context);
            var edited = await handler.Handle(editCommand, new CancellationToken());

            Assert.That(edited, Is.Not.Null);
            Assert.That(edited.Attribute.Name, Is.EqualTo(editCommand.Attribute.Name));
            Assert.That(edited.Attribute.Type, Is.EqualTo(editCommand.Attribute.Type));
            Assert.That(edited.Attribute.Value, Is.EqualTo(editCommand.Attribute.Value));
            Assert.That(edited.Attribute.Index, Is.EqualTo(editCommand.Attribute.Index));
        }

        [Test]
        public async Task Expect_Delete_Attribute()
        {
            // arrange
            var fixture = new TestFixture();
            var card = fixture.CreateCard(new Card {Title = "Test Card 1"});

            var createCmd = new Create.Command
            {
                Attribute = new Create.AttributeData
                {
                    Index = 0,
                    Name = "Description",
                    Type = "string",
                    Value = "Body of the test attribute"
                },
                CardId = card.Id
            };

            var created = await AttributeHelpers.CreateCardAttribute(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Attributes
                .Where(attribute => attribute.Id == deleteCmd.Id)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }
    }
}
