using System.IO;

namespace Spotacard.Core.Contracts
{
    public interface ICommand
    {
        DirectoryInfo Folder { get; }
        string Line { get; }
    }
}
