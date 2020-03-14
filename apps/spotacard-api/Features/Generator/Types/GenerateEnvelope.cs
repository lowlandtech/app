using System.Collections.Generic;

namespace Spotacard.Features.Generator.Types
{
    public class GenerateEnvelope
    {
        public List<FileData> Files { get; }

        public GenerateEnvelope(List<FileData> files)
        {
            Files = files;
        }
    }
}
