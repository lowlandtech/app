using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spotacard.Core.Enums;
using Spotacard.Infrastructure;

namespace Spotacard.Extensions
{
  public static class ProviderExtension
  {
    private const string SQLLITE = "ConnectionStrings:SqlLite";
    private const string PGSQL = "ConnectionStrings:PgSql";
    private const string PGSQL_ASSEMBLY = "Spotacard";
    private const string SQLSERVER = "ConnectionStrings:SqlServer";
    private const string SQLSERVER_ASSEMBLY = "Spotacard";

    public static void AddProvider(this IServiceCollection services)
    {
      switch (Startup.Settings.Provider)
      {
        case Providers.SqlLite:
          services.AddSqlLite();
          break;
        case Providers.PgSql:
          services.AddPgSql();
          break;
        case Providers.SqlServer:
          services.AddSqlServer();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static void AddPgSql(this IServiceCollection services)
    {
      var cs = Startup.Configuration[PGSQL];
      services
        .AddEntityFrameworkNpgsql()
        .AddDbContext<GraphContext>(options =>
        {
          options.UseNpgsql(cs, builder =>
            builder.MigrationsAssembly(PGSQL_ASSEMBLY));
        })
        .BuildServiceProvider();
    }

    private static void AddSqlLite(this IServiceCollection services)
    {
      var cs = Startup.Configuration[SQLLITE];
      services
        .AddDbContext<GraphContext>(
          options => options.UseSqlite(cs, builder =>
            builder.MigrationsAssembly(SQLSERVER_ASSEMBLY)));
    }

    private static void AddSqlServer(this IServiceCollection services)
    {
      var cs = Startup.Configuration[SQLSERVER];
      services
        .AddDbContext<GraphContext>(options =>
        {
          options.UseSqlServer(cs, builder =>
            builder.MigrationsAssembly(SQLSERVER_ASSEMBLY));
        })
        .BuildServiceProvider();
    }
  }
}
