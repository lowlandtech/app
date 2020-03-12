using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Cells.Types
{
    public class CellsEnvelope
    {
        public List<Cell> Cells { get; set; }
        public int CellsCount { get; set; }
    }
}
