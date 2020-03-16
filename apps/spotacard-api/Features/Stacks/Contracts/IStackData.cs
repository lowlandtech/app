using System;
using System.Threading.Tasks;
using Spotacard.Core.Enums;

namespace Spotacard.Features.Stacks.Contracts
{
    public interface IStackData
    {
        public string Name { get; set; }
        public string FileName { get; }
        public string Folder { get; }
        public StackTypes Type { get; set; }
        public Func<Task<string>> Execute { get;  }
    }
}
