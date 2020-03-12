using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Tables.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Tables.Commands
{
    public class ListById
    {
        public class Query : IRequest<TableEnvelope>
        {
            public Query(Guid tableId)
            {
                TableId = tableId;
            }

            public Guid TableId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.TableId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, TableEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<TableEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var table = await _context.Tables
                    .FirstOrDefaultAsync(x => x.Id == message.TableId, cancellationToken);

                if (table == null) throw new RestException(HttpStatusCode.NotFound, new { Table = Constants.NOT_FOUND });
                return new TableEnvelope(table);
            }
        }
    }
}
