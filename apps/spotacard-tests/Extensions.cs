using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Spotacard.Infrastructure;

namespace Spotacard
{
    public static class Extensions
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";

        public static void ReplaceGraphContext(this IServiceCollection services)
        {
            services.RemoveAll<GraphContext>();

            var connection = new SqliteConnection(InMemoryConnectionString);
            connection.Open();

            var builder = new DbContextOptionsBuilder<GraphContext>()
                .UseSqlite(connection);

            services.AddSingleton(new GraphContext(builder.Options));
        }
    }
}
