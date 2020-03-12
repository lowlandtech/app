using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Cells.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid CellId { get; }

            public Command(Guid cellId)
            {
                CellId = cellId;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CellId).NotNull().NotEmpty();
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
                var Cell = await _context.Cells
                    .FirstOrDefaultAsync(x => x.Id == message.CellId, cancellationToken);

                if (Cell == null) throw new RestException(HttpStatusCode.NotFound, new { Cell = Constants.NOT_FOUND });

                _context.Cells.Remove(Cell);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
