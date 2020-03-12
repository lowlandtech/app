using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Tables.Types
{
    public class TablesEnvelope
    {
        public List<Table> Tables { get; set; }
        public int TablesCount { get; set; }
    }
}
