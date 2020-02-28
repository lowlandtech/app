using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Cards
{
    public class List
    {
        public class Query : IRequest<CardsEnvelope>
        {
            public Query(string tag, string author, string favorited, int? limit, int? offset)
            {
                Tag = tag;
                Author = author;
                FavoritedUsername = favorited;
                Limit = limit;
                Offset = offset;
            }

            public string Tag { get; }
            public string Author { get; }
            public string FavoritedUsername { get; }
            public int? Limit { get; }
            public int? Offset { get; }
            public bool IsFeed { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, CardsEnvelope>
        {
            private readonly GraphContext _graph;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public QueryHandler(GraphContext graph, ICurrentUserAccessor currentUserAccessor)
            {
                _graph = graph;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<CardsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var queryable = _graph.Cards.GetAllData();

                if (message.IsFeed && _currentUserAccessor.GetCurrentUsername() != null)
                {
                    var currentUser = await _graph.Persons.Include(x => x.Following)
                        .FirstOrDefaultAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(),
                            cancellationToken);
                    queryable = queryable.Where(
                        x => currentUser.Following.Select(y => y.TargetId).Contains(x.Author.Id));
                }

                if (!string.IsNullOrWhiteSpace(message.Tag))
                {
                    var tag = await _graph.CardTags.FirstOrDefaultAsync(x => x.TagId == message.Tag,
                        cancellationToken);
                    if (tag != null)
                        queryable = queryable.Where(x => x.CardTags.Select(y => y.TagId).Contains(tag.TagId));
                    else
                        return new CardsEnvelope();
                }

                if (!string.IsNullOrWhiteSpace(message.Author))
                {
                    var author =
                        await _graph.Persons.FirstOrDefaultAsync(x => x.Username == message.Author,
                            cancellationToken);
                    if (author != null)
                        queryable = queryable.Where(x => x.Author == author);
                    else
                        return new CardsEnvelope();
                }

                if (!string.IsNullOrWhiteSpace(message.FavoritedUsername))
                {
                    var author =
                        await _graph.Persons.FirstOrDefaultAsync(x => x.Username == message.FavoritedUsername,
                            cancellationToken);
                    if (author != null)
                        queryable = queryable.Where(x => x.CardFavorites.Any(y => y.PersonId == author.Id));
                    else
                        return new CardsEnvelope();
                }

                var cards = await queryable
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(message.Offset ?? 0)
                    .Take(message.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new CardsEnvelope
                {
                    Cards = cards,
                    CardsCount = queryable.Count()
                };
            }
        }
    }
}
