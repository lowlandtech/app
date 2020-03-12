using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Apps.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid AppId { get; }

            public Command(Guid appId)
            {
                AppId = appId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AppId).NotNull().NotEmpty();
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
                var App = await _context.Apps
                    .FirstOrDefaultAsync(x => x.Id == message.AppId, cancellationToken);

                if (App == null) throw new RestException(HttpStatusCode.NotFound, new { App = Constants.NOT_FOUND });

                _context.Apps.Remove(App);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
