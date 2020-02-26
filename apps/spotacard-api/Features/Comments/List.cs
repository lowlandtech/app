using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Comments
{
    public class List
    {
        public class Query : IRequest<CommentsEnvelope>
        {
            public Query(string slug)
            {
                Slug = slug;
            }

            public string Slug { get; }
        }

        public class QueryHandler : IRequestHandler<Query, CommentsEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<CommentsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var card = await _context.Cards
                    .Include(x => x.Comments)
                        .ThenInclude(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (card == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });
                }

                return new CommentsEnvelope(card.Comments);
            }
        }
    }
}
