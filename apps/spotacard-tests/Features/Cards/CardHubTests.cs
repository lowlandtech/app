using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using NUnit.Framework;
using Spotacard.TestServer;

namespace Spotacard.Features.Cards
{
    public class CardHubTests
    {
        // [Test]
        public async Task ShouldReceiveMessage()
        {
            // Arrange
            var fixture = new TestFixture(graph => new CardData(graph));
            fixture.CreateClient();
            var server = fixture.Application.Server;
            var connection = await fixture.Application.StartConnectionAsync(server.CreateHandler(), "cards");

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
