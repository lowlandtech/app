using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Pages.Types;
using Spotacard.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Pages.Commands
{
    public class List
    {
        public class Query : IRequest<PagesEnvelope>
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

        public class QueryHandler : IRequestHandler<Query, PagesEnvelope>
        {
            private readonly GraphContext _context;
            private readonly ICurrentUser _currentUser;

            public QueryHandler(GraphContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<PagesEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var queryable = _context.Pages;

                var pages = await _context.Pages
                    .Skip(message.Offset ?? 0)
                    .Take(message.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new PagesEnvelope
                {
                    Pages = pages,
                    PagesCount = queryable.Count()
                };
            }
        }
    }
}
