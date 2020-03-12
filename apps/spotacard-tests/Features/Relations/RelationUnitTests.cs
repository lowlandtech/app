using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Relations.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Relations
{
    public class RelationUnitTests
    {
        [Test]
        public async Task Expect_Create_Relation()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            var command = new Create.Command
            {
                Relation = new Create.RelationData
                {
                    Name = "TestRelation"
                },
            };

            var relation = await RelationHelpers.CreateRelation(fixture, command);

            Assert.That(relation, Is.Not.Null);
            Assert.That(relation.Name, Is.EqualTo(command.Relation.Name));
        }

        [Test]
        public async Task Expect_Edit_Relation()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            try
            {
                var command = new Create.Command
                {
                    Relation = new Create.RelationData
                    {
                        Name = "TestRelation"
                    },
                };

                var created = await RelationHelpers.CreateRelation(fixture, command);
                var editCommand = new Edit.Command
                {
                    Relation = new Edit.RelationData
                    {
                        Name = "Updated " + created.Name
                    },
                    RelationId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Relation.Name, Is.EqualTo(editCommand.Relation.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Relation()
        {
            // arrange
            var fixture = new TestFixture(context => new GraphSeeder(context));
            var createCmd = new Create.Command
            {
                Relation = new Create.RelationData
                {
                    Name = "TestRelation"
                },
            };

            var created = await RelationHelpers.CreateRelation(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Relations
                .Where(relation => relation.Id == deleteCmd.RelationId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_List_Relation_To_Be_Empty()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.False(context.Relations.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Relation_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var relation = new Relation();
                context.Relations.Add(relation);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Relation_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var relation = new Relation
                {
                    Name = "TestRelation",
                };
                context.Relations.Add(relation);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(relation.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
