using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Apps.Types
{
    public class AppsEnvelope
    {
        public List<App> Apps { get; set; }
        public int AppsCount { get; set; }
    }
}
