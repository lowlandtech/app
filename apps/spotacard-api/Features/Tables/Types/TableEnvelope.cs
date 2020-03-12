using Spotacard.Domain;

namespace Spotacard.Features.Tables.Types
{
    public class TableEnvelope
    {
        public Table Table { get; }

        public TableEnvelope(Table table)
        {
            Table = table;
        }
    }
}
