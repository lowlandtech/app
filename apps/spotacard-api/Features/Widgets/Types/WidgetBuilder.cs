using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Widgets.Commands;
using Spotacard.Features.Widgets.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Widgets.Types
{
    internal class WidgetBuilder : IWidgetBuilder
    {
        private Widget _widget = new Widget();
        private readonly GraphContext _context;

        public WidgetBuilder(GraphContext context)
        {
            _context = context;
        }

        public IWidgetBuilder UseCreate(Create.WidgetData data)
        {
            _widget.Name = data.Name;
            _widget.Packages = data.Packages;
            _widget.Wiring = data.Wiring;
            _widget.CodeBehind = data.CodeBehind;
            _widget.Styling = data.Styling;
            _widget.Markup = data.Markup;
            _context.Widgets.Add(_widget);

            return this;
        }

        public IWidgetBuilder UseEdit(Edit.WidgetData data, Guid widgetId)
        {
            _widget = _context.Widgets
                .FirstOrDefault(widget => widget.Id == widgetId);

            if (_widget == null)
                throw new RestException(HttpStatusCode.NotFound, new { Widget = Constants.NOT_FOUND });

            _widget.Name = data.Name ?? _widget.Name;
            _widget.Packages = data.Packages ?? _widget.Packages;
            _widget.Wiring = data.Wiring ?? _widget.Wiring;
            _widget.CodeBehind = data.CodeBehind ?? _widget.CodeBehind;
            _widget.Styling = data.Styling ?? _widget.Styling;
            _widget.Markup = data.Markup ?? _widget.Markup;

            return this;
        }

        public IWidgetBuilder UseUser(ICurrentUser currentUser)
        {
            return this;
        }

        public async Task<WidgetEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new WidgetEnvelope(await _context.Widgets
                .Where(widget => widget.Id == _widget.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
