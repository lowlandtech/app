using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Cells.Commands;
using Spotacard.Features.Pages;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cells
{
    public class CellUnitTests
    {
        [Test]
        public async Task Expect_Create_Cell()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            var command = new Create.Command
            {
                Cell = new Create.CellData
                {
                    Name = "TestCell",
                    Area = "Area1",
                },
                PageId = TestCase1003.Page0.Id
            };

            var cell = await CellHelpers.CreateCell(fixture, command);

            Assert.That(cell, Is.Not.Null);
            Assert.That(cell.Name, Is.EqualTo(command.Cell.Name));
        }

        [Test]
        public async Task Expect_Edit_Cell()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            try
            {
                var command = new Create.Command
                {
                    Cell = new Create.CellData
                    {
                        Name = "TestCell",
                        Area = "Area1",
                    },
                    PageId = TestCase1003.Page0.Id
                };

                var created = await CellHelpers.CreateCell(fixture, command);
                var editCommand = new Edit.Command
                {
                    Cell = new Edit.CellData
                    {
                        Name = "Updated " + created.Name,
                        Area = "Updated " + created.Area
                    },
                    CellId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.Cell.Name, Is.EqualTo(editCommand.Cell.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_Cell()
        {
            // arrange
            var fixture = new TestFixture(context => new TestCase1005(context));
            var createCmd = new Create.Command
            {
                Cell = new Create.CellData
                {
                    Name = "TestCell",
                    Area = "Area1",
                },
                PageId = TestCase1003.Page0.Id
            };

            var created = await CellHelpers.CreateCell(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Cells
                .Where(cell => cell.Id == deleteCmd.CellId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_List_Cell_To_Be_Empty()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                Assert.False(context.Cells.Any());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Cell_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var cell = new Cell();
                context.Cells.Add(cell);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_Cell_Should_Generate_Id()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            try
            {
                var context = fixture.GetContext();
                var cell = new Cell
                {
                    Name = "TestCell",
                    Area = "Area1",
                    PageId = TestCase1003.Page0.Id
                };
                context.Cells.Add(cell);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(cell.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
