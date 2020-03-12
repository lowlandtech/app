using NUnit.Framework;
using Spotacard.Features.Relations.Commands;
using Spotacard.Features.Relations.Types;
using Spotacard.Infrastructure;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Spotacard.Features.Relations
{
    public class RelationIntegrationTests
    {
        [Test]
        public async Task Expect_List_Relation()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            try
            {
                // Arrange
                var uri = new Uri("relations", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<RelationsEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Relations.Count, Is.EqualTo(10));
                Assert.That(result.Relations.Find(relation => relation.Id == GraphSeeder.Relation1.Id), Is.Not.Null);
                Assert.That(result.Relations.Find(relation => relation.Id == GraphSeeder.Relation2.Id), Is.Not.Null);
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Relation_By_Id()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            try
            {
                // Arrange
                var uri = new Uri($"relations/{GraphSeeder.Relation1.Id}", UriKind.Relative);
                // Act
                var client = fixture.CreateClient();
                var response = await client.GetAsync(uri);
                var result = await fixture.Get<RelationEnvelope>(response);
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Relation.Id, Is.EqualTo(GraphSeeder.Relation1.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Create_Relation()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            try
            {
                // Arrange
                var uri = new Uri($"relations", UriKind.Relative);
                var command = new Create.Command
                {
                    Relation = new Create.RelationData
                    {
                        Name = "TestRelation",
                        PkFieldId = GraphSeeder.Relation0.PkFieldId,
                        PkName = "PkTestRelation",
                        FkFieldId = GraphSeeder.Relation0.PkFieldId,
                        FkName = "FkTestRelation",
                    },
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PostAsync(uri, fixture.Content(command));
                var result = await fixture.Get<RelationEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Relation, Is.Not.Null);
                Assert.That(result.Relation.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Relation.Name, Is.EqualTo("TestRelation"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Edit_Relation()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            const string updated = "Updated ";
            try
            {
                // Arrange
                var uri = new Uri($"relations/{GraphSeeder.Relation1.Id}", UriKind.Relative);
                var command = new Edit.Command
                {
                    Relation = new Edit.RelationData
                    {
                        Name = updated + GraphSeeder.Relation1.Name,
                        PkFieldId = GraphSeeder.Relation1.PkFieldId,
                        PkName = "PkTestRelation",
                        FkFieldId = GraphSeeder.Relation1.PkFieldId,
                        FkName = "FkTestRelation",
                    },
                    RelationId = GraphSeeder.Relation1.Id
                };

                // Act
                var client = fixture.CreateClient();
                var response = await client.PutAsync(uri, fixture.Content(command));
                var result = await fixture.Get<RelationEnvelope>(response);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Relation, Is.Not.Null);
                Assert.That(result.Relation.Name, Is.EqualTo(updated + GraphSeeder.Relation1.Name));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_List_Relation_By_Id_Not_Found()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            try
            {
                // Arrange
                var id = new Guid("9cf6d7b1-792a-4c7c-ac6d-a218ada3047d");
                var uri = new Uri($"relations/{id}", UriKind.Relative);
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
        public async Task Expect_Delete_Relation()
        {
            var fixture = new TestFixture(context => new GraphSeeder(context));
            try
            {
                // Arrange
                var uri = new Uri($"relations/{GraphSeeder.Relation1.Id}", UriKind.Relative);
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
