using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Widgets.Types
{
    public class WidgetsEnvelope
    {
        public List<Widget> Widgets { get; set; }
        public int WidgetsCount { get; set; }
    }
}
