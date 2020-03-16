using Spotacard.Core.Enums;
using Spotacard.Features.Stacks.Contracts;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Types.Base
{
    public abstract class StackItem : IStackData
    {
        protected StackItem(string root)
        {
            Folder = root ?? throw new ArgumentNullException(nameof(root));
        }

        public string FileName { get; }
        public string Folder { get; }
        public StackTypes Type { get; set; }
        public Func<Task<string>> Execute { get; protected set; }
        public string Name { get; set; }
    }
}
