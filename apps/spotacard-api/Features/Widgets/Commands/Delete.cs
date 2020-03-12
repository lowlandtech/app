using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Widgets.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid WidgetId { get; }

            public Command(Guid widgetId)
            {
                WidgetId = widgetId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.WidgetId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command message, CancellationToken cancellationToken)
            {
                var Widget = await _context.Widgets
                    .FirstOrDefaultAsync(x => x.Id == message.WidgetId, cancellationToken);

                if (Widget == null) throw new RestException(HttpStatusCode.NotFound, new { Widget = Constants.NOT_FOUND });

                _context.Widgets.Remove(Widget);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
