using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Relations.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Relations.Commands
{
    public class ListById
    {
        public class Query : IRequest<RelationEnvelope>
        {
            public Query(Guid relationId)
            {
                RelationId = relationId;
            }

            public Guid RelationId { get; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.RelationId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, RelationEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<RelationEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var relation = await _context.Relations
                    .FirstOrDefaultAsync(x => x.Id == message.RelationId, cancellationToken);

                if (relation == null) throw new RestException(HttpStatusCode.NotFound, new { Relation = Constants.NOT_FOUND });
                return new RelationEnvelope(relation);
            }
        }
    }
}
