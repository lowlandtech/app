using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spotacard.Core.Contracts;
using Spotacard.Infrastructure;

namespace Spotacard.Extensions
{
    public static class WebHostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                var seeder = services.GetRequiredService<ISeeder>();

                try
                {
                    logger.LogInformation("Seeding user data");
                    seeder.Execute();
                    logger.LogInformation("Seeded user data");
                }
                catch (Exception ex)
                {
                    logger.LogError("Error seeding user data - " + ex);
                }
            }

            return host;
        }

        public static IHost SeedGraphData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<GraphContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();


                try
                {
                    logger.LogInformation("Seeding graph data");
                    new GraphSeeder(context).Execute();
                    logger.LogInformation("seeded graph data");
                }
                catch (Exception ex)
                {
                    logger.LogError("Error seeding graph data - " + ex);
                }
            }

            return host;
        }

        public static IHost Migrate(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                var context = services.GetRequiredService<GraphContext>();
                var migrationsAssembly = context.GetService<IMigrationsAssembly>();
                var differ = context.GetService<IMigrationsModelDiffer>();

                var hasDifferences = differ.HasDifferences(
                    migrationsAssembly.ModelSnapshot.Model,
                    context.Model);

                if (!hasDifferences) return host;

                try
                {
                    logger.LogInformation("Migrating database");
                    context.Database.Migrate();
                    logger.LogInformation("Migrated database");
                }
                catch (Exception ex)
                {
                    logger.LogError("Error migrating database - " + ex);
                }
            }

            return host;
        }
    }
}
