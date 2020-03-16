using System;
using System.Threading.Tasks;
using Spotacard.Features.Stacks.Types.Base;

namespace Spotacard.Features.Stacks.Types
{
    public class StackFolder : StackItem
    {
        public StackFolder(string root, string filename, Func<Task<string>> action):base(root)
        {
    
        }
    }
}
