using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Relations.Types;
using Spotacard.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Relations.Commands
{
    public class List
    {
        public class Query : IRequest<RelationsEnvelope>
        {
            public Query(string author, int? limit, int? offset)
            {
                Author = author;
                Limit = limit;
                Offset = offset;
            }

            public string Author { get; }
            public int? Limit { get; }
            public int? Offset { get; }
        }

        public class QueryHandler : IRequestHandler<Query, RelationsEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public QueryHandler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<RelationsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var queryable = _context.Relations;

                var relations = await _context.Relations
                    .Skip(message.Offset ?? 0)
                    .Take(message.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new RelationsEnvelope
                {
                    Relations = relations,
                    RelationsCount = queryable.Count()
                };
            }
        }
    }
}
