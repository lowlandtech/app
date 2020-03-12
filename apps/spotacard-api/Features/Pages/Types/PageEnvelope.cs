using Spotacard.Domain;

namespace Spotacard.Features.Pages.Types
{
    public class PageEnvelope
    {
        public Page Page { get; }

        public PageEnvelope(Page page)
        {
            Page = page;
        }
    }
}
