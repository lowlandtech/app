using Spotacard.Domain;
using System.Collections.Generic;

namespace Spotacard.Features.Pages.Types
{
    public class PagesEnvelope
    {
        public List<Page> Pages { get; set; }
        public int PagesCount { get; set; }
    }
}
