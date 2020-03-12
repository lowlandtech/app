using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Fields.Commands;
using Spotacard.Features.Tables;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Fields
{
    public class FieldUnitTests
    {
        [Test]
        public async Task Expect_Create_Field()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            var command = new Create.Command
            {
                Field = new Create.FieldData
                {
                    Name = "TestField"
                },
                TableId = TestCase1002.Table0.Id
            };

            var field = await FieldHelpers.CreateField(fixture, command);

            Assert.That(field, Is.Not.Null);
            Assert.That(field.Name, Is.EqualTo(command.Field.Name));
        }

        [Test]
        public async Task Expect_Edit_Field()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            try
            {
                var command = new Create.Command
                {
                    Field = new Create.FieldData
                    {
                        Name = "TestField"
                    },
                    TableId = TestCase1002.Table0.Id
                };

                var created = await FieldHelpers.CreateField(fixture, command);
                var editCommand = new Edit.Command
                {
                    Field = new Edit.FieldData
                    {
                        Name = "Updated " + created.Name
                    },
                    FieldId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Field.Name, Is.EqualTo(editCommand.Field.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Field()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase1004(context));
            var createCmd = new Create.Command
            {
                Field = new Create.FieldData
                {
                    Name = "TestField"
                },
                TableId = TestCase1002.Table0.Id
            };

            var created = await FieldHelpers.CreateField(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Fields
                .Where(field => field.Id == deleteCmd.FieldId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_List_Field_To_Be_Empty()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.False(context.Fields.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Field_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var field = new Field();
                context.Fields.Add(field);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Field_Should_Generate_Id()
        {
            var fixture = new TestFixture(context => new TestCase1004(context));
            try
            {
                var context = fixture.GetContext();
                var field = new Field
                {
                    Name = "TestField",
                    TableId = TestCase1002.Table0.Id
                };
                context.Fields.Add(field);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(field.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
