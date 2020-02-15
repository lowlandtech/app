using Microsoft.AspNetCore.SignalR.Client;
using NUnit.Framework;
using Spotacard.TestServer;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Cards
{
  public class CardHubTests
  {
    [Test]
    public async Task ShouldReceiveMessage()
    {
      // Arrange
      var factory = TestServerFixture.Get<Startup>(graph => new CardData(graph));
      factory.CreateClient();
      var server = factory.Server;
      var connection = await factory.StartConnectionAsync(server.CreateHandler(), "cards");

      connection.Closed += async error =>
      { 
        await Task.Delay(new Random().Next(0, 5) * 1000);
        await connection.StartAsync();
      };

      // Act
      string user = null;
      string message = null;
      connection.On<string, string>("OnReceiveMessage", (u, m) =>
      {
        user = u;
        message = m;
      });
      await connection.InvokeAsync("SendMessage", "super_user", "Hello World!!");

      //Assert
      Assert.That(user, Is.EqualTo("super_user"));
      Assert.That(message, Is.EqualTo("Hello World!!"));
    }
  }
}
