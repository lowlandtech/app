using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spotacard.Core;
using Spotacard.Infrastructure;
using System;

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
          logger.LogInformation("Seeding database");
          seeder.Execute();
          logger.LogInformation("Seeding database");
        }
        catch (Exception ex)
        {
          logger.LogError("Error seeding database - " + ex);
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
        var graph = services.GetRequiredService<GraphContext>();

        try
        {
          logger.LogInformation("Migrating database");
          graph.Database.Migrate();
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
