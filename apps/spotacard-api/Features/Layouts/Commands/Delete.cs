using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Layouts.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid LayoutId { get; }

            public Command(Guid layoutId)
            {
                LayoutId = layoutId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.LayoutId).NotNull().NotEmpty();
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
                var Layout = await _context.Layouts
                    .FirstOrDefaultAsync(x => x.Id == message.LayoutId, cancellationToken);

                if (Layout == null) throw new RestException(HttpStatusCode.NotFound, new { Layout = Constants.NOT_FOUND });

                _context.Layouts.Remove(Layout);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
