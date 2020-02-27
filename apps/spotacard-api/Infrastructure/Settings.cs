using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;

namespace Spotacard.Infrastructure
{
    public class Settings : ISettings
    {
        private const string PROVIDER = "Provider";
        private const string REPOSITORIES = "Repositories";
        private const string PLUGINPATH = "PluginsPath";
        private readonly IWebHostEnvironment _hosting;

        public Settings(IConfiguration configuration, IWebHostEnvironment hosting)
        {
            Configuration = configuration;
            _hosting = hosting;
            Plugins = configuration.GetSection("Plugins").Get<List<string>>();
            Provider = configuration.GetValue<Providers>(PROVIDER);
            var provider = configuration[REPOSITORIES];
            if (provider != null) Repositories = new DirectoryInfo(provider);
            PluginPath = configuration[PLUGINPATH];
        }

        public IConfiguration Configuration { get; }

        public string[] GetTranslationFile(string translations)
        {
            return File.ReadAllLines(Path.Combine(_hosting.ContentRootPath, translations));
        }

        public bool IsDevelopment()
        {
            return _hosting.IsDevelopment();
        }

        public DirectoryInfo Repositories { get; set; }
        public Providers Provider { get; set; }
        public List<string> Plugins { get; set; }
        public string PluginPath { get; set; }
    }
}
