using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Layouts.Commands;
using Spotacard.Features.Layouts.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Layouts.Types
{
    internal class LayoutBuilder : ILayoutBuilder
    {
        private Layout _layout = new Layout();
        private readonly GraphContext _context;

        public LayoutBuilder(GraphContext context)
        {
            _context = context;
        }

        public ILayoutBuilder UseCreate(Create.LayoutData data)
        {
            _layout.Name = data.Name;
            _layout.Packages = data.Packages;
            _layout.Wiring = data.Wiring;
            _layout.CodeBehind = data.CodeBehind;
            _layout.Styling = data.Styling;
            _layout.Markup = data.Markup;
            _context.Layouts.Add(_layout);

            return this;
        }

        public ILayoutBuilder UseEdit(Edit.LayoutData data, Guid layoutId)
        {
            _layout = _context.Layouts
                .FirstOrDefault(layout => layout.Id == layoutId);

            if (_layout == null)
                throw new RestException(HttpStatusCode.NotFound, new { Layout = Constants.NOT_FOUND });

            _layout.Name = data.Name ?? _layout.Name;
            _layout.Packages = data.Packages ?? _layout.Packages;
            _layout.Wiring = data.Wiring ?? _layout.Wiring;
            _layout.CodeBehind = data.CodeBehind ?? _layout.CodeBehind;
            _layout.Styling = data.Styling ?? _layout.Styling;
            _layout.Markup = data.Markup ?? _layout.Markup;

            return this;
        }

        public ILayoutBuilder UseUser(ICurrentUser currentUser)
        {
            return this;
        }

        public async Task<LayoutEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new LayoutEnvelope(await _context.Layouts
                .Where(layout => layout.Id == _layout.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
