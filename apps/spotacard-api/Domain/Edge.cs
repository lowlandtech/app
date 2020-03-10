using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotacard.Core.Enums;

namespace Spotacard.Domain
{
    public class Edge
    {
        public EdgeLabels Label { get; set; }
        public int Index { get; set; }
        public Card Parent { get; set; }
        public Guid ParentId { get; set; }
        public Card Child { get; set; }
        public Guid ChildId { get; set; }
    }
}
