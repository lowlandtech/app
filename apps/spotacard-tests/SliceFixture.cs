using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spotacard.Infrastructure;

namespace Spotacard
{
  public class SliceFixture : IDisposable
  {
    private static readonly IConfiguration Config;
    private readonly string _dbName = Guid.NewGuid() + ".db";
    private readonly ServiceProvider _provider;

    private readonly IServiceScopeFactory _scopeFactory;

    static SliceFixture()
    {
      Config = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();
    }

    public SliceFixture()
    {
      var startup = new Startup(Config, null);
      var services = new ServiceCollection();

      var builder = new DbContextOptionsBuilder<GraphContext>();
      builder.UseInMemoryDatabase(_dbName);
      services.AddSingleton(new GraphContext(builder.Options));

      startup.ConfigureServices(services);

      _provider = services.BuildServiceProvider();

      GetGraph().Database.EnsureCreated();
      _scopeFactory = _provider.GetService<IServiceScopeFactory>();
    }

    public void Dispose()
    {
      File.Delete(_dbName);
    }

    public GraphContext GetGraph()
    {
      return _provider.GetRequiredService<GraphContext>();
    }

    public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
      using var scope = _scopeFactory.CreateScope();
      await action(scope.ServiceProvider);
    }

    public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
      using var scope = _scopeFactory.CreateScope();
      return await action(scope.ServiceProvider);
    }

    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
      return ExecuteScopeAsync(sp =>
      {
        var mediator = sp.GetService<IMediator>();

        return mediator.Send(request);
      });
    }

    public Task SendAsync(IRequest request)
    {
      return ExecuteScopeAsync(sp =>
      {
        var mediator = sp.GetService<IMediator>();
        return mediator.Send(request);
      });
    }

    public Task ExecuteDbContextAsync(Func<GraphContext, Task> action)
    {
      return ExecuteScopeAsync(sp => action(sp.GetService<GraphContext>()));
    }

    public Task<T> ExecuteDbContextAsync<T>(Func<GraphContext, Task<T>> action)
    {
      return ExecuteScopeAsync(sp => action(sp.GetService<GraphContext>()));
    }

    public Task InsertAsync(params object[] entities)
    {
      return ExecuteDbContextAsync(db =>
      {
        foreach (var entity in entities) db.Add(entity);
        return db.SaveChangesAsync();
      });
    }
  }
}
