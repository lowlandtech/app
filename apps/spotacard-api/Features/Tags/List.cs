using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Tags
{
    public class List
    {
        public class Query : IRequest<TagsEnvelope>
        {
        }

        public class QueryHandler : IRequestHandler<Query, TagsEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<TagsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var tags = await _context.Tags.OrderBy(x => x.TagId).AsNoTracking().ToListAsync(cancellationToken);
                return new TagsEnvelope
                {
                    Tags = tags.Select(x => x.TagId).ToList()
                };
            }
        }
    }
}
