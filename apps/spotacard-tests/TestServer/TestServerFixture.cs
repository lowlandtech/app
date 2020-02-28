using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spotacard.TestServer
{
    public static class TestServerFixture
    {
        public static async Task<HubConnection> StartConnectionAsync<TStartup>(
            this WebApplicationFactory<TStartup> factory,
            HttpMessageHandler handler, string hubName) where TStartup : class
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl($"ws://localhost/hubs/{hubName}", o => { o.HttpMessageHandlerFactory = _ => handler; })
                .Build();
            await hubConnection.StartAsync();
            return hubConnection;
        }
    }
}
