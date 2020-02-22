using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spotacard.Extensions;

namespace Spotacard
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args)
        .Build()
        .Migrate()
        .SeedData()
        .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      var config = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();

      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder =>
        {
          builder
            .UseConfiguration(config)
            .UseSerilog()
            .UseKestrel()
            .UseUrls("http://+:5000")
            .UseStartup<Startup>();
        });
    }
  }
}
