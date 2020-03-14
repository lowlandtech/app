using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Contents.Types
{
    public class ContentsEnvelope
    {
        public List<Content> Contents { get; set; }
        public int ContentsCount { get; set; }
    }
}
