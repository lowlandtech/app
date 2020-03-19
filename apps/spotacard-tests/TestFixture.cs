using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Features.Graphs;
using Spotacard.Features.Users;
using Spotacard.Infrastructure;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Spotacard
{
    public class TestFixture : IDisposable
    {
        private static readonly string Root = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory);
        private readonly Func<GraphContext, IActivity> _seed;
        private static readonly IConfiguration Config;
        public string Token { get; set; }

        private readonly string _inMemoryConnectionString = $"DataSource=file:{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}?mode=memory";

        private void ReplaceGraphContext(IServiceCollection services)
        {
            services.RemoveAll<GraphContext>();

            var connection = new SqliteConnection(_inMemoryConnectionString);
            connection.Open();

            var builder = new DbContextOptionsBuilder<GraphContext>()
                .UseSqlite(connection);

            var settings = new Settings(null, null)
            {
                Repositories = new DirectoryInfo(Path.Combine(Root, "repositories"))
            };

            services.AddSingleton<ISettings>(settings);
            services.AddSingleton(new GraphContext(builder.Options));
        }

        static TestFixture()
        {
            Config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            // use sqlite;
            Config["Provider"] = "1";
        }

        /// <summary>
        /// Starts a test server to run tests against ...
        /// </summary>
        /// <param name="seed"></param>
        public TestFixture(Func<GraphContext, IActivity> seed = null)
        {
            _seed = seed ?? throw new ArgumentNullException(nameof(seed));

            Application = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(ReplaceGraphContext);
                });

            Provider = Application.Services;
            Step1();
            Step2();
            Step3();
        }

        public TestFixture()
        {
            var startup = new Startup(Config, null);
            var services = new ServiceCollection();
            startup.Settings.Provider = Providers.SqlLite;
            startup.Settings.Repositories = new DirectoryInfo(Path.Combine(Root, "repositories"));
            startup.ConfigureServices(services);
            ReplaceGraphContext(services);
            Provider = services.BuildServiceProvider();
            Step1();
            Step2();
        }

        public WebApplicationFactory<Startup> Application { get; }
        public IServiceScopeFactory Factory { get; set; }
        public IServiceProvider Provider { get; set; }

        public void Dispose()
        {
            GetContext().Database.CloseConnection();
        }

        private void Step1()
        {
            Factory = Provider.GetService<IServiceScopeFactory>();
            GetContext().Database.EnsureCreated();
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
                    Email = "admin@spotacard.com",
                    Password = "admin"
                }
            });
            Token = result.Result.User.Token;
        }

        private void Step3()
        {
            if (_seed == null) return;
            var activity = _seed(GetContext());
            activity.Execute();
        }

        public Card CreateCard(Card card)
        {
            var context = GetContext();
            context.Cards.Add(card);
            context.SaveChanges();
            return card;
        }

        public CardAttribute CreateAttribute(CardAttribute attribute)
        {
            var context = GetContext();
            context.Attributes.Add(attribute);
            context.SaveChanges();
            return attribute;
        }

        public GraphContext GetContext()
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

        public async Task<T> Get<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public IGraph GetGraph()
        {
            var scope = Factory.CreateScope();
            var services = scope.ServiceProvider;
            return services.GetService<IGraph>();
        }
    }
}
