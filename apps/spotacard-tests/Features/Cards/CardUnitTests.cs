using System;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Cards.Commands;
using Spotacard.Features.Cards.Types;

namespace Spotacard.Features.Cards
{
    public class CardUnitTests
    {
        [Test]
        public async Task Expect_Create_Card()
        {
            var fixture = new TestFixture();
            var command = new Create.Command
            {
                Card = new Create.CardData
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = "tag1,tag2"
                }
            };

            var card = await CardHelpers.CreateCard(fixture, command);

            Assert.That(card, Is.Not.Null);
            Assert.That(card.Title, Is.EqualTo(command.Card.Title));
            Assert.That(card.TagList.Count(), Is.EqualTo(command.Card.TagList.Split(",").Count()));
        }

        [Test]
        public async Task Expect_Edit_Card()
        {
            var fixture = new TestFixture();
            try
            {
                var command = new Create.Command
                {
                    Card = new Create.CardData
                    {
                        Title = "Test card dsergiu77",
                        Description = "Description of the test card",
                        Body = "Body of the test card",
                        TagList = "tag1,tag2"
                    }
                };

                var created = await CardHelpers.CreateCard(fixture, command);
                var editCommand = new Edit.Command
                {
                    Card = new Edit.CardData
                    {
                        Title = "Updated " + created.Title,
                        Description = "Updated" + created.Description,
                        Body = "Updated" + created.Body
                    },
                    Slug = created.Slug
                };

                // remove the first tag and add a new tag
                editCommand.Card.TagList = $"{created.TagList[1]}, tag3";

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Card.Title, Is.EqualTo(editCommand.Card.Title));
                Assert.That(edited.Card.TagList.Count(), Is.EqualTo(editCommand.Card.TagList.Split(",").Count()));
                // use assert Contains because we do not know the order in which the tags are saved/retrieved
                Assert.That(editCommand.Card.TagList.Contains(edited.Card.TagList[0]), Is.True);
                Assert.That(editCommand.Card.TagList.Contains(edited.Card.TagList[1]), Is.True);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Card()
        {
            // arrange
            var fixture = new TestFixture();
            var createCmd = new Create.Command
            {
                Card = new Create.CardData
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card"
                }
            };

            var created = await CardHelpers.CreateCard(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Slug);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Cards
                .Where(_card => _card.Slug == deleteCmd.Slug)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task Expect_Delete_Card_With_Tags()
        {
            // arrange
            var fixture = new TestFixture();
            var command = new Create.Command
            {
                Card = new Create.CardData
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = "tag1,tag2"
                }
            };

            var created = await CardHelpers.CreateCard(fixture, command);
            var deleteCmd = new Delete.Command(created.Slug);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Cards
                .Where(d => d.Slug == deleteCmd.Slug)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task Expect_Delete_Card_With_Comments()
        {
            // arrange
            var fixture = new TestFixture();
            var command = new Create.Command
            {
                Card = new Create.CardData
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card"
                }
            };

            var created = await CardHelpers.CreateCard(fixture, command);
            var deleteCmd = new Delete.Command(created.Slug);

            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Cards
                .Where(card => card.Slug == deleteCmd.Slug)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_Service_Card_To_Save()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var service = new CardService(context);
                service.Add("https://example.com");
                context.SaveChanges();

                Assert.That(1, Is.EqualTo(context.Cards.Count()));
                Assert.That("https://example.com", Is.EqualTo(context.Cards.Single().Title));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_List_Card_To_Be_Empty()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.False(context.Cards.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Card_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var card = new Card();
                context.Cards.Add(card);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Card_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var card = new Card { Title = "TestCard" };
                context.Cards.Add(card);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(card.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Find_Card_By_Id()
        {
            // In-memory database only exists while the connection is open
            var fixture = new TestFixture();

            try
            {
                var context = fixture.GetContext();

                context.Cards.Add(new Card { Title = "https://example.com/cats" });
                context.Cards.Add(new Card { Title = "https://example.com/catfish" });
                context.Cards.Add(new Card { Title = "https://example.com/dogs" });
                context.SaveChanges();

                var service = new CardService(context);
                var result = service.Find("cat");
                Assert.That(2, Is.EqualTo(result.Count()));
            }
            finally
            {
                fixture.Dispose();
            }
        }
        [Test]
        public async Task Expect_Create_Card_To_Slug()
        {
            var fixture = new TestFixture();
            var command = new Create.Command
            {
                Card = new Create.CardData
                {
                    Title = "Test card dsergiu77",
                    Description = "Description of the test card",
                    Body = "Body of the test card",
                    TagList = "tag1,tag2"
                }
            };

            var card = await CardHelpers.CreateCard(fixture, command);

            Assert.That(card, Is.Not.Null);
            Assert.That(card.Slug, Is.EqualTo("test-card-dsergiu77"));
        }
    }
}
