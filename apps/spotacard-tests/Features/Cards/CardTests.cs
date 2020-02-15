using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Graphs;
using Spotacard.Infrastructure;
using System;
using System.Linq;

namespace Spotacard.Features.Cards
{
  public class CardTests
  {
    [Test]
    public void ShouldWriteCardToDatabase()
    {
      var response = GraphFixtures.Get();

      try
      {
        var options = new DbContextOptionsBuilder<GraphContext>()
          .UseSqlite(response.Connection)
          .Options;

        // Create the schema in the database
        using (var graph = new GraphContext(options))
        {
          graph.Database.EnsureCreated();
        }

        // Run the test against one instance of the graph
        using (var graph = new GraphContext(options))
        {
          var service = new CardService(graph);
          service.Add("https://example.com");
          graph.SaveChanges();
        }

        // Use a separate instance of the graph to verify correct data was saved to database
        using (var graph = new GraphContext(options))
        {
          Assert.That(1, Is.EqualTo(graph.Cards.Count()));
          Assert.That("https://example.com", Is.EqualTo(graph.Cards.Single().Title));
        }
      }
      finally
      {
        response.Connection.Close();
      }
    }

    [Test]
    public void ShouldBeEmpty()
    {
      var response = GraphFixtures.Get();
      try
      {
        Assert.False(response.Graph.Cards.Any());
      }
      finally
      {
        response.Connection.Close();
      }
    }

    [Test]
    public void ShouldBeRequiredName()
    {
      var response = GraphFixtures.Get();
      try
      {
        var card = new Card();
        response.Graph.Cards.Add(card);
        Assert.Throws<DbUpdateException>(() => response.Graph.SaveChanges());
      }
      finally
      {
        response.Connection.Close();
      }
    }

    [Test]
    public void ShouldGenerateCardId()
    {
      var response = GraphFixtures.Get();
      try
      {
        var card = new Card { Title = "TestCard" };
        response.Graph.Cards.Add(card);
        response.Graph.SaveChanges();

        Assert.That(Guid.Empty, Is.Not.EqualTo(card.Id));
      }
      finally
      {
        response.Connection.Close();
      }
    }

    [Test]
    public void ShouldFindCardByName()
    {
      // In-memory database only exists while the connection is open
      var connection = new SqliteConnection("DataSource=:memory:");
      connection.Open();

      try
      {
        var options = new DbContextOptionsBuilder<GraphContext>()
          .UseSqlite(connection)
          .Options;

        // Create the schema in the database
        using (var graph = new GraphContext(options))
        {
          graph.Database.EnsureCreated();
        }

        // Insert seed data into the database using one instance of the graph
        using (var graph = new GraphContext(options))
        {
          graph.Cards.Add(new Card { Title = "https://example.com/cats" });
          graph.Cards.Add(new Card { Title = "https://example.com/catfish" });
          graph.Cards.Add(new Card { Title = "https://example.com/dogs" });
          graph.SaveChanges();
        }

        // Use a clean instance of the graph to run the test
        using (var graph = new GraphContext(options))
        {
          var service = new CardService(graph);
          var result = service.Find("cat");
          Assert.That(2, Is.EqualTo(result.Count()));
        }
      }
      finally
      {
        connection.Close();
      }
    }
  }
}
