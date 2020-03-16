using RazorLight;
using Spotacard.Domain;

namespace Spotacard.Features.Stacks.Types
{
    public class StackContext
    {
        public RazorLightEngine Engine1 { get; set; }
        public RazorLightEngine Engine2 { get; set; }
        public App App { get; set; }
        public Stack Stack { get; set; }
        public string Root { get; set; }
    }
}
