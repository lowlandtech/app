using Spotacard.Domain;
using Spotacard.Features.Templates.Enums;
using System;

namespace Spotacard.Features.Templates.ViewModels
{
    public class TemplateVm
    {
        private readonly Card _card;
        
        public TemplateVm(Card card)
        {
            _card = card;
            Type = Enum.Parse<TemplateTypes>(_card.Get(TemplateFields.Type).ToString(), true);
            FileName = _card.Get(TemplateFields.FileName).ToString();
            Id = _card.Id.ToString();
        }

        public string Id { get; }
        public string FileName { get; }
        public TemplateTypes Type { get; }
    }
}
