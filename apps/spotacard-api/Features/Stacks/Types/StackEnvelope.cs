using Spotacard.Domain;

namespace Spotacard.Features.Stacks.Types
{
    public class StackEnvelope
    {
        public Stack Stack { get; }

        public StackEnvelope(Stack stack)
        {
            Stack = stack;
        }
    }
}
