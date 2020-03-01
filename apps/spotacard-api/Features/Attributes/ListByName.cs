using MediatR;
using Microsoft.EntityFrameworkCore;
using Spotacard.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace Spotacard.Features.Attributes
{
    public class ListByName
    {
        public class Query : IRequest<AttributeEnvelope>
        {
            public string Name { get; }
            public Guid CardId { get; }

            public Query(Guid cardId, string name)
            {
                Name = name;
                CardId = cardId;
            }

        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
                RuleFor(x => x.CardId).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, AttributeEnvelope>
        {
            private readonly GraphContext _context;

            public QueryHandler(GraphContext context)
            {
                _context = context;
            }

            public async Task<AttributeEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var attribute = await _context.Attributes
                    .SingleOrDefaultAsync(_ =>
                        _.CardId == request.CardId &&
                        _.Name == request.Name,
                        cancellationToken
                    );
                return new AttributeEnvelope(attribute);
            }
        }
    }
}
