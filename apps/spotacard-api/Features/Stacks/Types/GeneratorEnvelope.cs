using Spotacard.Features.Stacks.Types.Base;
using System.Collections.Generic;

namespace Spotacard.Features.Stacks.Types
{
    public class GeneratorEnvelope
    {
        public List<StackItem> Items { get; }

        public GeneratorEnvelope(List<StackItem> items)
        {
            Items = items;
        }
    }
}
