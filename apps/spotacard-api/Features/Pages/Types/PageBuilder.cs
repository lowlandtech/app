using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Pages.Commands;
using Spotacard.Features.Pages.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Pages.Types
{
    internal class PageBuilder : IPageBuilder
    {
        private Page _page = new Page();
        private readonly GraphContext _context;

        public PageBuilder(GraphContext context)
        {
            _context = context;
        }

        public IPageBuilder UseCreate(Create.PageData data, Guid appId)
        {
            _page.Name = data.Name;
            _page.AppId = appId;

            _context.Pages.Add(_page);

            return this;
        }

        public IPageBuilder UseEdit(Edit.PageData data, Guid pageId)
        {
            _page = _context.Pages
                .FirstOrDefault(x => x.Id == pageId);

            if (_page == null)
                throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });

            _page.Name = data.Name ?? _page.Name;

            return this;
        }

        public IPageBuilder UseUser(ICurrentUser currentUser)
        {

            return this;
        }

        public async Task<PageEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new PageEnvelope(await _context.Pages
                .Where(x => x.Id == _page.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
