using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Spotacard.Core;
using Spotacard.Infrastructure;

namespace Spotacard.TestServer
{
  public static class TestServerFixture
  {
    public static async Task<HubConnection> StartConnectionAsync<TStartup>(this WebApplicationFactory<TStartup> factory,
      HttpMessageHandler handler, string hubName) where TStartup : class
    {
      var hubConnection = new HubConnectionBuilder()
        .WithUrl($"ws://localhost/hubs/{hubName}", o => { o.HttpMessageHandlerFactory = _ => handler; })
        .Build();
      await hubConnection.StartAsync();
      return hubConnection;
    }

    public static WebApplicationFactory<TStartup> Get<TStartup>(Func<GraphContext, IActivity> seed = null)
      where TStartup : class
    {
      IActivity activity;
      return new WebApplicationFactory<TStartup>()
        .WithWebHostBuilder(builder =>
        {
          builder.ConfigureServices(services =>
          {
            services.RemoveAll<GraphContext>();
            services.AddDbContext<GraphContext>(options => { options.UseSqlite("DataSource=:memory:"); });

            if (seed == null) return;
            var graphServiceProvider = services.BuildServiceProvider();
            using var scope = graphServiceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;
            using var graph = scopedProvider.GetRequiredService<GraphContext>();
            activity = seed(graph);
            activity.Execute();
          });
        });
    }
  }
}
