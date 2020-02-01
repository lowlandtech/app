using System.Collections.Generic;

namespace Spotacard.Domain
{
    public class Tag
    {
        public string TagId { get; set; }

        public List<CardTag> CardTags { get; set; }
    }
}