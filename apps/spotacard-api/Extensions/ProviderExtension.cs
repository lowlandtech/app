using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Infrastructure;
using System;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Spotacard.Extensions
{
    public static class ProviderExtension
  {
    private const string SQLLITE = "ConnectionStrings:SqlLite";
    private const string PGSQL = "ConnectionStrings:PgSql";
    private const string PGSQL_ASSEMBLY = "Spotacard";
    private const string SQLSERVER = "ConnectionStrings:SqlServer";
    private const string SQLSERVER_ASSEMBLY = "Spotacard";

    public static void AddProvider(this IServiceCollection services, ISettings settings)
    {
      switch (settings.Provider)
      {
        case Providers.SqlLite:
          services.AddSqlLite(settings.Configuration);
          break;
        case Providers.PgSql:
          services.AddPgSql(settings.Configuration);
          break;
        case Providers.SqlServer:
          services.AddSqlServer(settings.Configuration);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static void AddPgSql(this IServiceCollection services, IConfiguration configuration)
    {
      var cs = configuration[PGSQL];
      services
        .AddEntityFrameworkNpgsql()
        .AddDbContext<GraphContext>(options =>
        {
          options.UseNpgsql(cs, builder =>
            builder.MigrationsAssembly(PGSQL_ASSEMBLY));
        })
        .BuildServiceProvider();
    }

    private static void AddSqlLite(this IServiceCollection services, IConfiguration configuration)
        {
      var cs = configuration[SQLLITE];
      services
        .AddDbContext<GraphContext>(
          options => options.UseSqlite(cs, builder =>
            builder.MigrationsAssembly(SQLSERVER_ASSEMBLY)));
    }

    private static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
      var cs = configuration[SQLSERVER];
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
