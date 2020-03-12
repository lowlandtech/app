using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Tables.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid TableId { get; }

            public Command(Guid tableId)
            {
                TableId = tableId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.TableId).NotNull().NotEmpty();
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
                var Table = await _context.Tables
                    .FirstOrDefaultAsync(x => x.Id == message.TableId, cancellationToken);

                if (Table == null) throw new RestException(HttpStatusCode.NotFound, new { Table = Constants.NOT_FOUND });

                _context.Tables.Remove(Table);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
