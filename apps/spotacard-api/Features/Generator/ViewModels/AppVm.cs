using Spotacard.Domain;

namespace Spotacard.Features.Generator.ViewModels
{
    public class AppVm
    {
        private readonly Card _card;

        public AppVm(Card card)
        {
            _card = card;
        }
    }
}
