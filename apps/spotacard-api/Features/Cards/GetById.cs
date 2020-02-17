using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Spotacard.Features.Cards
{
    public class GetById
    {
        public class Query : IRequest<CardEnvelope>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
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
                    .FirstOrDefaultAsync(x => x.Id == message.Id, cancellationToken);

                if (card == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });
                }
                return new CardEnvelope(card);
            }
        }
    }
}
