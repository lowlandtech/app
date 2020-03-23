using MediatR;
using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;

namespace LowLandTech.Appish
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddMediatR(typeof(Program));

            await builder.Build().RunAsync();
        }
    }
}
