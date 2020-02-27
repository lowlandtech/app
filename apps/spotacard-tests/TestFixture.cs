using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;

namespace Spotacard
{
    public class TestFixture : IDisposable
    {
        private static readonly IConfiguration Config;

        static TestFixture()
        {
            Config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            // use sqlite;
            Config["Provider"] = "1";
        }

        public TestFixture(Func<GraphContext, IActivity> seed = null)
        {
            Application = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services => { services.ReplaceGraphContext(); });
                });

            Step(Application.Services);

            if (seed == null) return;
            var activity = seed(GetGraph());
            activity.Execute();
        }

        public TestFixture()
        {
            var startup = new Startup(Config, null);
            var services = new ServiceCollection();
            startup.Settings.Provider = Providers.SqlLite;
            startup.ConfigureServices(services);
            services.ReplaceGraphContext();

            Step(services.BuildServiceProvider());
        }

        public WebApplicationFactory<Startup> Application { get; }
        public IServiceScopeFactory Factory { get; set; }
        public IServiceProvider Provider { get; set; }

        public void Dispose()
        {
        }

        public void Step(IServiceProvider provider)
        {
            Provider = provider;
            Factory = Provider.GetService<IServiceScopeFactory>();
            GetGraph().Database.EnsureCreated();
        }

        public Card CreateCard(Card card)
        {
            var graph = GetGraph();
            graph.Cards.Add(card);
            graph.SaveChanges();
            return card;
        }

        public CardAttribute CreateAttribute(CardAttribute attribute)
        {
            var graph = GetGraph();
            graph.Attributes.Add(attribute);
            graph.SaveChanges();
            return attribute;
        }

        public GraphContext GetGraph()
        {
            return Provider.GetRequiredService<GraphContext>();
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = Factory.CreateScope();
            await action(scope.ServiceProvider);
        }

        public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using var scope = Factory.CreateScope();
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
