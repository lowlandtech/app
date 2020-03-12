using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Layouts.Types
{
    public class LayoutsEnvelope
    {
        public List<Layout> Layouts { get; set; }
        public int LayoutsCount { get; set; }
    }
}
