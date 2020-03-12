using Spotacard.Domain;

namespace Spotacard.Features.Fields.Types
{
    public class FieldEnvelope
    {
        public Field Field { get; }

        public FieldEnvelope(Field field)
        {
            Field = field;
        }
    }
}
