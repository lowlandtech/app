using Spotacard.Domain;

namespace Spotacard.Features.Contents.Types
{
    public class ContentEnvelope
    {
        public Content Content { get; }

        public ContentEnvelope(Content content)
        {
            Content = content;
        }
    }
}
