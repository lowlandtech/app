using System.Collections.Generic;
using System.IO;
using Spotacard.Core.Enums;

namespace Spotacard.Core.Contracts
{
  public interface ISettings
  {
    bool IsDevelopment();
    string[] GetTranslationFile(string path);
    DirectoryInfo Repositories { get; }
    Providers Provider { get; }
    List<string> Plugins { get; set; }
    string PluginPath { get; set; }
  }
}
