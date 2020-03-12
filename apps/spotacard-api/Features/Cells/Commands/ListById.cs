using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Cells.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cells.Commands
{
    public class ListById
    {
        public class Query : IRequest<CellEnvelope>
        {
            public Query(Guid cellId)
            {
                CellId = cellId;
            }

            public Guid CellId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.CellId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, CellEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<CellEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var cell = await _context.Cells
                    .FirstOrDefaultAsync(x => x.Id == message.CellId, cancellationToken);

                if (cell == null) throw new RestException(HttpStatusCode.NotFound, new { Cell = Constants.NOT_FOUND });
                return new CellEnvelope(cell);
            }
        }
    }
}
