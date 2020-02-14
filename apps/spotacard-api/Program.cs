using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spotacard.Extensions;

namespace Spotacard
{
  public class Program
  {
    private static IConfigurationRoot _config
      ;

    public static void Main(string[] args)
    {
      _config = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();

      CreateHostBuilder(args)
        .Build()
        .Migrate()
        .SeedData()
        .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseConfiguration(_config)
            .UseSerilog()
            .UseKestrel()
            .UseUrls($"http://+:5000")
            .UseStartup<Startup>();
        });
    }
  }
}
