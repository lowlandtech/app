using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorLight.Razor;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Templates
{
    public class Project : RazorLightProject
    {
        private readonly GraphContext _context;

        public Project(GraphContext context)
        {
            _context = context;
        }

        public override async Task<RazorLightProjectItem> GetItemAsync(string templateKey)
        {
            var id = new Guid(templateKey);
            var card = await _context.Cards.FindAsync(id);
            var projectItem = new Template(templateKey, card?.Description);

            return projectItem;
        }

        public override Task<IEnumerable<RazorLightProjectItem>> GetImportsAsync(string templateKey)
        {
            return Task.FromResult(Enumerable.Empty<RazorLightProjectItem>());
        }
    }
}
