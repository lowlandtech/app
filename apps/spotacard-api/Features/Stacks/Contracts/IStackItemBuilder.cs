using Spotacard.Domain;
using Spotacard.Features.Stacks.Types.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Contracts
{
    public interface IStackItemBuilder<in T>
    {
        IStackItemBuilder<T> Use(Stack stack, T data);
        IStackItemBuilder<T> UseRoot(string root);
        Task<List<StackItem>> BuildAsync();
    }
}
