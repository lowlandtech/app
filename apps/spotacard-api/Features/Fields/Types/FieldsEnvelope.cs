using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Fields.Types
{
    public class FieldsEnvelope
    {
        public List<Field> Fields { get; set; }
        public int FieldsCount { get; set; }
    }
}
