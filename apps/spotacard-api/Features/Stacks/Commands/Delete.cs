using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Stacks.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid StackId { get; }

            public Command(Guid stackId)
            {
                StackId = stackId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StackId).NotNull().NotEmpty();
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
                var Stack = await _context.Stacks
                    .FirstOrDefaultAsync(x => x.Id == message.StackId, cancellationToken);

                if (Stack == null) throw new RestException(HttpStatusCode.NotFound, new { Stack = Constants.NOT_FOUND });

                _context.Stacks.Remove(Stack);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
