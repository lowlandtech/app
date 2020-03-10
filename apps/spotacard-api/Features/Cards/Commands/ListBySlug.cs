using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Features.Cards.Types;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;

namespace Spotacard.Features.Cards.Commands
{
    public class ListBySlug
    {
        public class Query : IRequest<CardEnvelope>
        {
            public Query(string slug)
            {
                Slug = slug;
            }

            public string Slug { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, CardEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<CardEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var card = await _context.Cards.GetAllData()
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (card == null) throw new RestException(HttpStatusCode.NotFound, new {Card = Constants.NOT_FOUND});
                return new CardEnvelope(card);
            }
        }
    }
}
