using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Stacks.Types
{
    public class StacksEnvelope
    {
        public List<Stack> Stacks { get; set; }
        public int StacksCount { get; set; }
    }
}
