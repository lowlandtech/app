using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Features.Users;
using Spotacard.Infrastructure;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spotacard
{
    public class TestFixture : IDisposable
    {
        private static readonly IConfiguration Config;
        public string Token { get; set; }

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

            Provider = Application.Services;
            Step1();
            Step2();

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
            Provider = services.BuildServiceProvider();

            Step1();
            Step2();
        }

        public WebApplicationFactory<Startup> Application { get; }
        public IServiceScopeFactory Factory { get; set; }
        public IServiceProvider Provider { get; set; }

        public void Dispose()
        {
        }

        private void Step1()
        {
            Factory = Provider.GetService<IServiceScopeFactory>();
            GetGraph().Database.EnsureCreated();
        }

        private void Step2()
        {
            using var scope = Provider.CreateScope();
            var services = scope.ServiceProvider;
            var seeder = services.GetRequiredService<ISeeder>();
            seeder.Execute();
            var mediator = services.GetRequiredService<IMediator>();
            var result = mediator.Send(new Login.Command
            {
                User = new Login.UserData
                {
                    Email = "admin@spotacard",
                    Password = "admin"
                }
            });
            Token = result.Result.User.Token;
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

        public HttpClient CreateClient()
        {
            var client = Application.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public HttpContent Content<T>(T command)
        {
            return new StringContent(JsonConvert.SerializeObject(command), Encoding.Default, "application/json");
        }
    }
}
