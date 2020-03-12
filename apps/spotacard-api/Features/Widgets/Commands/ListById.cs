using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Widgets.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Widgets.Commands
{
    public class ListById
    {
        public class Query : IRequest<WidgetEnvelope>
        {
            public Query(Guid widgetId)
            {
                WidgetId = widgetId;
            }

            public Guid WidgetId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.WidgetId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, WidgetEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<WidgetEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var widget = await _context.Widgets
                    .FirstOrDefaultAsync(x => x.Id == message.WidgetId, cancellationToken);

                if (widget == null) throw new RestException(HttpStatusCode.NotFound, new { Widget = Constants.NOT_FOUND });
                return new WidgetEnvelope(widget);
            }
        }
    }
}
