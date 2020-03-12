using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Relations.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid RelationId { get; }

            public Command(Guid relationId)
            {
                RelationId = relationId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RelationId).NotNull().NotEmpty();
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
                var Relation = await _context.Relations
                    .FirstOrDefaultAsync(x => x.Id == message.RelationId, cancellationToken);

                if (Relation == null) throw new RestException(HttpStatusCode.NotFound, new { Relation = Constants.NOT_FOUND });

                _context.Relations.Remove(Relation);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
