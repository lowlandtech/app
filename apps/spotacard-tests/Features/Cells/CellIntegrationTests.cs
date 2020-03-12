using NUnit.Framework;
using Spotacard.Features.Cells.Commands;
using Spotacard.Features.Cells.Types;
using Spotacard.Features.Pages;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Cells
{
    public class CellIntegrationTests
    {
        [Test]
        public async Task Expect_List_Cell()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            try
            {
                // Arrange
                var uri = new Uri("cells", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<CellsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Cells.Count, Is.EqualTo(10));
                Assert.That(result.Cells.Find(cell => cell.Id == TestCase1005.Cell1.Id), Is.Not.Null);
                Assert.That(result.Cells.Find(cell => cell.Id == TestCase1005.Cell2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Cell_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            try
            {
                // Arrange
                var uri = new Uri($"cells/{TestCase1005.Cell1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<CellEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Cell.Id, Is.EqualTo(TestCase1005.Cell1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Cell()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            try
            {
                // Arrange
                var uri = new Uri($"cells", UriKind.Relative);
                var command = new Create.Command
                {
                    Cell = new Create.CellData
                    {
                        Name = "TestCell",
                        Area = "Area1",
                    },
                    PageId = TestCase1003.Page0.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<CellEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Cell, Is.Not.Null);
                Assert.That(result.Cell.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Cell.Name, Is.EqualTo("TestCell"));
                Assert.That(result.Cell.Area, Is.EqualTo("Area1"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Cell()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"cells/{TestCase1005.Cell1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Cell = new Edit.CellData
                    {
                        Name = updated + TestCase1005.Cell1.Name
                    },
                    CellId = TestCase1005.Cell1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<CellEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Cell, Is.Not.Null);
                Assert.That(result.Cell.Name, Is.EqualTo(updated + TestCase1005.Cell1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Cell_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"cells/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Cell()
        {
            var fixture = new TestFixture(context => new TestCase1005(context));
            try
            {
                // Arrange
                var uri = new Uri($"cells/{TestCase1005.Cell1.Id}", UriKind.Relative);
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
