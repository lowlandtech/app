using Spotacard.Domain;

namespace Spotacard.Features.Widgets.Types
{
    public class WidgetEnvelope
    {
        public Widget Widget { get; }

        public WidgetEnvelope(Widget widget)
        {
            Widget = widget;
        }
    }
}
