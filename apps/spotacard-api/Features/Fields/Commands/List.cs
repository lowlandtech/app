using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Fields.Types;
using Spotacard.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Fields.Commands
{
    public class List
    {
        public class Query : IRequest<FieldsEnvelope>
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

        public class QueryHandler : IRequestHandler<Query, FieldsEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public QueryHandler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<FieldsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var queryable = _context.Fields;

                var fields = await _context.Fields
                    .Skip(message.Offset ?? 0)
                    .Take(message.Limit ?? 100)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new FieldsEnvelope
                {
                    Fields = fields,
                    FieldsCount = queryable.Count()
                };
            }
        }
    }
}
