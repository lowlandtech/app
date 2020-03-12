using Spotacard.Domain;

namespace Spotacard.Features.Cells.Types
{
    public class CellEnvelope
    {
        public Cell Cell { get; }

        public CellEnvelope(Cell cell)
        {
            Cell = cell;
        }
    }
}
