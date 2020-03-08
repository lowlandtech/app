using Spotacard.Features.Templates.Contracts;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Templates
{
    internal class TemplateService : ITemplateService
    {
        private readonly GraphContext _context;

        public TemplateService(GraphContext context)
        {
            _context = context;
        }
    }
}
