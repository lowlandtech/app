using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Attributes
{
    public class List
    {
        public class Query : IRequest<AttributesEnvelope>
        {
            public Query(Guid cardId)
            {
                CardId = cardId;
            }

            public Guid CardId { get; }
        }

        public class QueryHandler : IRequestHandler<Query, AttributesEnvelope>
        {
            private readonly GraphContext _graph;

            public QueryHandler(GraphContext graph)
            {
                _graph = graph;
            }

            public async Task<AttributesEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var attributes = await _graph.Attributes
                    .Where(attribute => attribute.CardId == request.CardId)
                    .ToListAsync(cancellationToken);
                return new AttributesEnvelope(attributes);
            }
        }
    }
}
