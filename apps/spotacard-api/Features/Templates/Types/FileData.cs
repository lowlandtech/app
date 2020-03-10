using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Templates.Types
{
    public class FileData
    {
        public string Name { get; }
        public Func<Task<string>> GetContent { get; }

        public FileData(string name, Func<Task<string>> getContent)
        {
            Name = name;
            GetContent = getContent;
        }
    }
}
