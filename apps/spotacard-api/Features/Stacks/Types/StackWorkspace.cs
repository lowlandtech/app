using Spotacard.Features.Stacks.Types.Base;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Types
{
    public class StackWorkspace : StackItem
    {
        public StackWorkspace(string root, string name, Func<Task<string>> execute) : base(root)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
