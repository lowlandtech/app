using NUnit.Framework;
using Spotacard.Features.Tables.Commands;
using Spotacard.Features.Tables.Types;
using System;
using System.Net;
using System.Threading.Tasks;
using Spotacard.Features.Apps;

namespace Spotacard.Features.Tables
{
    public class TableIntegrationTests
    {
        [Test]
        public async Task Expect_List_Table()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            try
            {
                // Arrange
                var uri = new Uri("tables", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<TablesEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Tables.Count, Is.EqualTo(10));
                Assert.That(result.Tables.Find(table => table.Id == TestCase1002.Table1.Id), Is.Not.Null);
                Assert.That(result.Tables.Find(table => table.Id == TestCase1002.Table2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Table_By_Id()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            try
            {
                // Arrange
                var uri = new Uri($"tables/{TestCase1002.Table1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<TableEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Table.Id, Is.EqualTo(TestCase1002.Table1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Table()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            try
            {
                // Arrange
                var uri = new Uri($"tables", UriKind.Relative);
                var command = new Create.Command
                {
                    Table = new Create.TableData
                    {
                        Name = "TestTable",
                    },
                    AppId = TestCase1001.App0.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<TableEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Table, Is.Not.Null);
                Assert.That(result.Table.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Table.Name, Is.EqualTo("TestTable"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Table()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"tables/{TestCase1002.Table1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Table = new Edit.TableData
                    {
                        Name = updated + TestCase1002.Table1.Name
                    },
                    TableId = TestCase1002.Table1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<TableEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Table, Is.Not.Null);
                Assert.That(result.Table.Name, Is.EqualTo(updated + TestCase1002.Table1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Table_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"tables/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Table()
        {
            var fixture = new TestFixture(context => new TestCase1002(context));
            try
            {
                // Arrange
                var uri = new Uri($"tables/{TestCase1002.Table1.Id}", UriKind.Relative);
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
