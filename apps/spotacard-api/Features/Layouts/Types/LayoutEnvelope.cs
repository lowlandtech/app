using Spotacard.Domain;

namespace Spotacard.Features.Layouts.Types
{
    public class LayoutEnvelope
    {
        public Layout Layout { get; }

        public LayoutEnvelope(Layout layout)
        {
            Layout = layout;
        }
    }
}
