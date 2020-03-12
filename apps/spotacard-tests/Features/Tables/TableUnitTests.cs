using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Apps;
using Spotacard.Features.Tables.Commands;

namespace Spotacard.Features.Tables
{
    public class TableUnitTests
    {
        [Test]
        public async Task Expect_Create_Table()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            var command = new Create.Command
            {
                Table = new Create.TableData
                {
                    Name = "TestTable"
                },
                AppId = TestCase1001.App0.Id
            };

            var table = await TableHelpers.CreateTable(fixture, command);

            Assert.That(table, Is.Not.Null);
            Assert.That(table.Name, Is.EqualTo(command.Table.Name));
        }

        [Test]
        public async Task Expect_Edit_Table()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            try
            {
                var command = new Create.Command
                {
                    Table = new Create.TableData
                    {
                        Name = "TestTable"
                    },
                    AppId = TestCase1001.App0.Id
                };

                var created = await TableHelpers.CreateTable(fixture, command);
                var editCommand = new Edit.Command
                {
                    Table = new Edit.TableData
                    {
                        Name = "Updated " + created.Name
                    },
                    TableId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Table.Name, Is.EqualTo(editCommand.Table.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Table()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase1002(context));
            var createCmd = new Create.Command
            {
                Table = new Create.TableData
                {
                    Name = "TestTable"
                },
                AppId = TestCase1001.App0.Id
            };

            var created = await TableHelpers.CreateTable(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Tables
                .Where(table => table.Id == deleteCmd.TableId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_Create_Table_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var table = new Table();
                context.Tables.Add(table);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Table_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var table = new Table
                {
                    Name = "TestTable",
                    App = TestCase1001.App0,
                    AppId = TestCase1001.App0.Id
                };
                context.Tables.Add(table);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(table.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
