using Spotacard.Core.Contracts;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spotacard.Core.Types
{
    public class Shell : IShell
    {
        private readonly ISettings _settings;

        public Shell(ISettings settings)
        {
            _settings = settings;
        }

        public string Execute()
        {
            var results = new StringBuilder();
            foreach (var command in Commands)
            {
                if (!command.Folder.Exists) command.Folder.Create();
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c {command.Line}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = command.Folder.FullName
                    }
                };

                process.Start();
                results.AppendLine(process.StandardOutput.ReadToEnd());
                process.WaitForExit();
            }

            return results.ToString();
        }

        public List<ICommand> Commands { get; set; } = new List<ICommand>();
    }
}
