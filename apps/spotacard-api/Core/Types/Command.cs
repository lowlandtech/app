using System;
using System.IO;
using Spotacard.Core.Contracts;

namespace Spotacard.Core.Types
{
    public class Command : ICommand
    {
        public Command(string folder, string line)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));
            Line = line ?? throw new ArgumentNullException(nameof(line));
            Folder = new DirectoryInfo(folder);
        }

        public DirectoryInfo Folder { get; }
        public string Line { get; }
    }
}
